DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_bulk_tinward`(
  in in_file_gid int,
  in in_inward_gid int,
  in in_comp_code varchar(32),
  in in_inward_no int,
  in in_received_date date,
  in in_tran_code char(2),
  in in_folio_no varchar(32),
  in in_shareholder_name varchar(64),
  in in_shareholder_pan_no varchar(16),
  in in_share_count int(10),
  in in_inward_remark varchar(255),
  in in_action varchar(16),
  in in_action_by varchar(16),
  in in_line_no int,
  in in_errline_flag boolean,
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare v_tran_others_flag char(1) default '';
  declare v_folio_gid int default 0;
  declare v_tran_folio_gid int default 0;
  declare v_inward_no int default 0;
  declare v_inward_gid int default 0;
  declare v_inward_received_status int default 0;
  declare v_queue_maker_status int default 0;
  
  declare v_comp_short_code varchar(16);
  declare v_inward_sno varchar(16);
  declare v_inward_comp_no varchar(16);
  declare v_inward_comp_sno varchar(16);
  declare v_comp_gid int;
  declare v_courier_gid int default 25;
  declare v_received_mode char(1) default 'C';
  declare v_awb_no varchar(32) default '0987654321';
  declare v_entity_gid int default 1;
  declare v_cert_gid text;
  declare v_share_count int(10);

  declare v_action_success_status tinyint default 0;
  declare v_action_reject_status tinyint default 0;
  declare v_ret int default 0;

 DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;

    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
    
     if in_errline_flag = true then
      call pr_sta_ins_errline(in_file_gid,in_line_no,out_msg);
    end if;
  END;

  set err_msg = concat('Error',' -- ');
  
  select comp_gid into v_comp_gid 
  from sta_mst_tcompany where comp_code = in_comp_code
  and delete_flag='N';
    
  if v_comp_gid = 0 then
    set err_msg := concat(err_msg,'Invalid company code,');
    set err_flag := true;
  end if;
  
  select status_value into v_action_success_status from sta_mst_tstatus
  where status_type = 'Action'
  and status_desc = 'Ok'
  and delete_flag = 'N';

  set v_action_success_status := ifnull(v_action_success_status,0);

  if v_action_success_status = 0 then
    set err_msg := concat(err_msg,'Action success status to be maintained,');
    set err_flag := true;
  end if;

  select status_value into v_action_reject_status from sta_mst_tstatus
  where status_type = 'Action'
  and status_desc = 'Reject'
  and delete_flag = 'N';

  set v_action_reject_status := ifnull(v_action_reject_status,0);

  if v_action_reject_status = 0 then
    set err_msg := concat(err_msg,'Action failure status to be maintained,');
    set err_flag := true;
  end if;

  select status_value into v_inward_received_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Received'
  and delete_flag = 'N';

  set v_inward_received_status = ifnull(v_inward_received_status,0);

  if v_inward_received_status = 0 then
    set err_msg := concat(err_msg,'Inward received status not maintained,');
    set err_flag := true;
  end if;

  select status_value into v_queue_maker_status from sta_mst_tstatus
  where status_type = 'Queue'
  and status_desc = 'Maker'
  and delete_flag = 'N';

  set v_queue_maker_status = ifnull(v_queue_maker_status,0);

  if v_queue_maker_status = 0 then
    set err_msg := concat(err_msg,'Queue maker status not maintained,');
    set err_flag := true;
  end if;

  if in_received_date is null then
    set err_msg := concat(err_msg,'Received date cannot be blank,');
    set err_flag := true;
  end if;

  if not exists(select receivedmode_gid from sta_mst_treceivedmode
      where receivedmode_code = v_received_mode
      and delete_flag = 'N') then
    set err_msg := concat(err_msg,'Invalid received mode,');
    set err_flag := true;
  end if;
  
  if not exists(select courier_gid from sta_mst_tcourier
      where courier_gid = v_courier_gid
      and delete_flag = 'N') and in_tran_code <> 'DT' then
    set err_msg := concat(err_msg,'Invalid courier,');
    set err_flag := true;
  end if;
  
  if v_awb_no = '' then
    set err_msg := concat(err_msg,'Awb no cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_folio_no = '' then
    set err_msg := concat(err_msg,'Folio no cannot be blank,');
    set err_flag := true;
  end if;

  if not exists(  select trantype_gid from sta_mst_ttrantype
				  where trantype_code = in_tran_code
				  and delete_flag = 'N') then
    set err_msg := concat(err_msg,'Invalid tran code,');
    set err_flag := true;
  else
    select others_flag into v_tran_others_flag from sta_mst_ttrantype
    where trantype_code = in_tran_code
    and delete_flag = 'N';
  end if;
  
	select inward_sno,comp_short_code into  v_inward_sno,v_comp_short_code from sta_mst_tcompany where comp_gid=v_comp_gid and delete_flag='N';
    set v_inward_comp_sno=(select LPAD(v_inward_sno,'4','0'));
    set v_inward_comp_no=concat(v_comp_short_code,v_inward_comp_sno);
    

  if v_tran_others_flag = 'N' then
    
    if not exists(select comp_gid from sta_mst_tcompany
        where comp_gid = v_comp_gid
        and delete_flag = 'N') then
      set err_msg := concat(err_msg,'Invalid company,');
      set err_flag := true;
    end if;

    
    if not exists(select folio_gid from sta_trn_tfolio
        where comp_gid = v_comp_gid
        and folio_no = in_folio_no
        and delete_flag = 'N') then
      set err_msg := concat(err_msg,'Invalid folion no,');
      set err_flag := true;
    else
      select folio_gid,holder1_name,holder1_pan_no 
      into v_folio_gid,in_shareholder_name,in_shareholder_pan_no
      from sta_trn_tfolio
      where comp_gid = v_comp_gid
      and folio_no = in_folio_no
      and delete_flag = 'N';
    end if;
  end if;
  
  if in_shareholder_pan_no <> '' then
		select fn_sta_chk_panno(in_shareholder_pan_no) into v_ret;
	if v_ret = 0 then
		  set err_msg := concat(err_msg,'Invalid pan no,');
		  set err_flag := true;
	end if;
  end if;
  
  if in_shareholder_name = '' then
    set err_msg := concat(err_msg,'Share holder name cannot be blank,');
    set err_flag := true;
  end if;
  
if err_flag = true then
    set out_result = 0;
    set out_msg = err_msg;

    if in_errline_flag = true then
      call pr_sta_ins_errline(in_file_gid,in_line_no,out_msg);
    end if;

    leave me;
  end if;

  if in_action = 'INSERT' then
    if err_flag = false then
      set v_inward_no = ifnull(in_inward_no,0);

      if v_inward_no = 0 then
        select max(inward_no) into v_inward_no from sta_trn_tinward
        where entity_gid = v_entity_gid
        and delete_flag = 'N';

        set v_inward_no = ifnull(v_inward_no,0);
        set v_inward_no = v_inward_no + 1;
      else
        
        if exists(select inward_gid from sta_trn_tinward
          where entity_gid = v_entity_gid
          and inward_no = v_inward_no
          and delete_flag = 'N') then
          set err_msg := concat(err_msg,'Duplicate inward no,');
          set err_flag := true;
        end if;
      end if;
    end if;
    
    if in_tran_code = 'LS' then
		 select
			  group_concat(c.cert_gid),
			  sum(c.share_count) into v_cert_gid,v_share_count
			from sta_trn_tcert as c
			left join sta_mst_tstatus as s on s.status_value = c.cert_status and s.status_type = 'Certificate' and s.delete_flag = 'N'
			where c.folio_gid = v_folio_gid
			and c.cert_status <> 'N'
			and c.delete_flag = 'N'
			order by c.cert_no;
	end if;
    
	if in_share_count != v_share_count then
		set err_msg := concat(err_msg,'Share count Missmatch,');
		set err_flag := true;
		call pr_sta_ins_errline(in_file_gid,in_line_no,err_msg);
        set out_result = 0;
		set out_msg = err_msg;
     end if;

    if err_flag = false then
      START TRANSACTION;

      INSERT INTO sta_trn_tinward
      (
        entity_gid,comp_gid,inward_no,inward_comp_no,inward_comp_sno,received_date,received_mode,courier_gid,awb_no,tran_code,docsubtype_code,
        shareholder_name,inward_status,inward_all_status,queue_status,queue_all_status,
        folio_gid,tran_folio_gid,folio_no,shareholder_addr,shareholder_pan_no,shareholder_contact_no,shareholder_email_id,
        inward_remark,insert_date,insert_by,share_count
      ) values
      (
        v_entity_gid,v_comp_gid,v_inward_no,v_inward_comp_no,v_inward_sno,in_received_date,v_received_mode,v_courier_gid,v_awb_no,in_tran_code,'',
        in_shareholder_name,v_inward_received_status,v_inward_received_status,v_queue_maker_status,v_queue_maker_status,
        v_folio_gid,v_tran_folio_gid,in_folio_no,'',in_shareholder_pan_no,'','',
        in_inward_remark,sysdate(),in_action_by,0
      );

		UPDATE sta_mst_tcompany SET inward_sno=inward_sno+1 WHERE comp_gid=v_comp_gid and delete_flag='N';
      COMMIT;

      select max(inward_gid) into v_inward_gid from sta_trn_tinward;
      
      if in_tran_code = 'LS' then
        call pr_sta_ins_queue(v_inward_gid,in_action_by,'N','M','',@queue_gid,@out,@msg);
        call pr_sta_set_certentry(v_inward_gid,v_cert_gid,127,0,'Bulk',1,'system',@out_result,@out_msg);
		set out_result = 1;
		set out_msg = 'Record inserted successfully';
      end if;
      
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;
end if;
  
END$$
DELIMITER ;
