DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_bulk_tinward_ot`(
  in in_file_gid int,
  inout in_inward_gid int,
  in in_entity_gid int,
  in in_comp_code varchar(32),
  in in_inward_no int,
  in in_received_date date,
  in in_received_mode char(1),
  in in_courier_gid int,
  in in_awb_no varchar(32),
  in in_tran_code char(2),
  in in_docsubtype_code char(2),
  in in_folio_no varchar(32),
  in in_shareholder_name varchar(64),
  in in_shareholder_addr varchar(255),
  in in_shareholder_pan_no varchar(16),
  in in_shareholder_contact_no varchar(128),
  in in_shareholder_email_id varchar(128),
  in in_share_count int,
  in in_inward_remark varchar(255),
  in in_email_subject varchar(255),
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
  declare v_comp_gid int default 0;
  declare v_tran_folio_gid int default 0;
  declare v_inward_no int default 0;
  declare v_inward_gid int default 0;
  declare v_inward_received_status int default 0;
  declare v_queue_maker_status int default 0;
  
  declare v_comp_short_code varchar(16);
  declare v_inward_sno varchar(16);
  declare v_inward_comp_no varchar(16);
  declare v_inward_comp_sno varchar(16);

  declare v_action_success_status tinyint default 0;
  declare v_action_reject_status tinyint default 0;
  declare v_ret int default 0;

  /*DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;

    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;*/
  
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

  set err_msg = concat('Error',char(13),char(10),'-----');
  set in_inward_gid = 0;
  
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
    set err_msg := concat(err_msg,char(13),char(10),'Inward received status not maintained,');
    set err_flag := true;
  end if;

  select status_value into v_queue_maker_status from sta_mst_tstatus
  where status_type = 'Queue'
  and status_desc = 'Maker'
  and delete_flag = 'N';

  set v_queue_maker_status = ifnull(v_queue_maker_status,0);

  if v_queue_maker_status = 0 then
    set err_msg := concat(err_msg,char(13),char(10),'Queue maker status not maintained,');
    set err_flag := true;
  end if;

  if in_received_date is null then
    set err_msg := concat(err_msg,char(13),char(10),'Received date cannot be blank,');
    set err_flag := true;
  end if;
  
  set in_received_mode = 'E';
  if not exists(select receivedmode_gid from sta_mst_treceivedmode
      where receivedmode_code = in_received_mode
      and delete_flag = 'N') then
    set err_msg := concat(err_msg,char(13),char(10),'Invalid received mode,');
    set err_flag := true;
  end if;
  
  set in_tran_code = 'OT';
  select courier_gid into in_courier_gid 
  from sta_mst_tcourier 
  where courier_name = 'E-MAIL'
  and delete_flag = 'N';
  
  if not exists(select courier_gid from sta_mst_tcourier
      where courier_gid = in_courier_gid
      and delete_flag = 'N') and in_tran_code <> 'DT' then
    set err_msg := concat(err_msg,char(13),char(10),'Invalid courier,');
    set err_flag := true;
  end if;
  
  set in_awb_no = '-';
  if in_awb_no = '' then
    set err_msg := concat(err_msg,char(13),char(10),'Awb no cannot be blank,');
    set err_flag := true;
  end if;
  
  #Get Share holder name 
  select holder1_name into in_shareholder_name 
  from sta_trn_tfolio where comp_gid = v_comp_gid
  and folio_no = in_folio_no and delete_flag = 'N';
  
  if in_folio_no = '' and in_shareholder_name = '' then
		set err_msg := concat(err_msg,char(13),char(10),'Shareholder Name cannot be blank,');
		set err_flag := true;
  end if;

  if in_shareholder_pan_no <> '' then
		select fn_sta_chk_panno(in_shareholder_pan_no) into v_ret;
	if v_ret = 0 then
		  set err_msg := concat(err_msg,char(13),char(10),'Invalid pan no,');
		  set err_flag := true;
	end if;
  end if;
  
  if not exists(  select trantype_gid from sta_mst_ttrantype
				  where trantype_code = in_tran_code
				  and delete_flag = 'N') then
    set err_msg := concat(err_msg,char(13),char(10),'Invalid tran code,');
    set err_flag := true;
  else
    select others_flag into v_tran_others_flag from sta_mst_ttrantype
    where trantype_code = in_tran_code
    and delete_flag = 'N';
  end if;
  
  set in_docsubtype_code = 'EM'; 
  if in_tran_code = 'OT' and  in_docsubtype_code = '' then
		set err_msg := concat(err_msg,char(13),char(10),'Please select Document Subtype,');
		set err_flag := true;
  end if;
  
	if in_tran_code = 'OT' and  in_docsubtype_code <> '' then
		if not exists(	select docsubtype_code from sta_mst_tdocsubtype
						where docsubtype_code = in_docsubtype_code
						and delete_flag = 'N') then
			set err_msg := concat(err_msg,char(13),char(10),'Invalid Document Subtype,');
			set err_flag := true;
      end if;
	end if;
    
  if in_shareholder_email_id = '' then
    set err_msg := concat(err_msg,char(13),char(10),'Mail Id cannot be blank,');
    set err_flag := true;
  end if;
  
  select comp_gid into v_comp_gid 
  from sta_mst_tcompany where comp_code = in_comp_code
  and delete_flag='N';
  
  if v_comp_gid = 0 then
    set err_msg := concat(err_msg,'Invalid company code,');
    set err_flag := true;
  end if;
  
	select inward_sno,comp_short_code into  v_inward_sno,v_comp_short_code from sta_mst_tcompany where comp_gid=v_comp_gid and delete_flag='N';
    set v_inward_comp_sno=(select LPAD(v_inward_sno,'4','0'));
    set v_inward_comp_no=concat(v_comp_short_code,v_inward_comp_sno);
 
  if v_tran_others_flag = 'N' then
    if not exists(select comp_gid from sta_mst_tcompany
        where comp_gid = v_comp_gid
        and delete_flag = 'N') then
      set err_msg := concat(err_msg,char(13),char(10),'Invalid company,');
      set err_flag := true;
    end if;

    if not exists(select folio_gid from sta_trn_tfolio
        where comp_gid = v_comp_gid
        and folio_no = in_folio_no
        and delete_flag = 'N') then
      set err_msg := concat(err_msg,char(13),char(10),'Invalid folio no,');
      set err_flag := true;
    else
      select folio_gid into v_folio_gid from sta_trn_tfolio
      where comp_gid = v_comp_gid
      and folio_no = in_folio_no
      and delete_flag = 'N';
    end if;
  end if;
  
  /*if in_shareholder_name = '' then
    set err_msg := concat(err_msg,char(13),char(10),'Share holder name cannot be blank,');
    set err_flag := true;
  end if;*/
  
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
	  set in_entity_gid = 1;
      if v_inward_no = 0 then
        select max(inward_no) into v_inward_no from sta_trn_tinward
        where entity_gid = in_entity_gid
        and delete_flag = 'N';

        set v_inward_no = ifnull(v_inward_no,0);
        set v_inward_no = v_inward_no + 1;
      else
        
        if exists(select inward_gid from sta_trn_tinward
          where entity_gid = in_entity_gid
          and inward_no = v_inward_no
          and delete_flag = 'N') then
          set err_msg := concat(err_msg,char(13),char(10),'Duplicate inward no,');
          set err_flag := true;
        end if;
      end if;
    end if;

    if err_flag = false then
      START TRANSACTION;

      INSERT INTO sta_trn_tinward
      (
        entity_gid,comp_gid,inward_no,inward_comp_no,inward_comp_sno,received_date,received_mode,courier_gid,awb_no,tran_code,docsubtype_code,
        shareholder_name,inward_status,inward_all_status,queue_status,queue_all_status,
        folio_gid,tran_folio_gid,folio_no,shareholder_addr,shareholder_pan_no,shareholder_contact_no,shareholder_email_id,
        inward_remark,email_subject,insert_date,insert_by,share_count
      ) values
      (
        in_entity_gid,v_comp_gid,v_inward_no,v_inward_comp_no,v_inward_sno,in_received_date,in_received_mode,in_courier_gid,in_awb_no,in_tran_code,in_docsubtype_code,
        in_shareholder_name,v_inward_received_status,v_inward_received_status,v_queue_maker_status,v_queue_maker_status,
        v_folio_gid,v_tran_folio_gid,in_folio_no,in_shareholder_addr,in_shareholder_pan_no,in_shareholder_contact_no,in_shareholder_email_id,
        in_inward_remark,in_email_subject,sysdate(),in_action_by,in_share_count
      );

		UPDATE sta_mst_tcompany SET inward_sno=inward_sno+1 WHERE comp_gid=v_comp_gid and delete_flag='N';
        
      COMMIT;

      select max(inward_gid) into v_inward_gid from sta_trn_tinward;
      
      if in_tran_code <> 'OT' then
        call pr_sta_ins_queue(v_inward_gid,in_action_by,'N','M','',@queue_gid,@out,@msg);
      else
        call pr_sta_ins_queue(v_inward_gid,in_action_by,'N','D','',@queue_gid,@out,@msg);
      end if;
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;

    -- set out_inward_no = v_inward_no;
    -- set out_comp_inward_no=v_inward_comp_no;
    set in_inward_gid = v_inward_gid; 
    set out_result = 1;
    set out_msg = 'Record inserted successfully';
    leave me;
  end if;

END$$
DELIMITER ;
