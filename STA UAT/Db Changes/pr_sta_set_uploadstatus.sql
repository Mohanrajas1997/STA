DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_uploadstatus`(
  in in_upload_gid int,
  in in_approved_date date,
  in in_remark varchar(255),
  in in_upload_status int,
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  
  declare v_comp_gid int ;

  declare v_upload_gid int default 0;
  declare v_upload_status tinyint default 0;
  declare v_upload_done_status tinyint default 0;
  declare v_upload_success_status tinyint default 0;
  declare v_upload_failure_status tinyint default 0;

  declare v_upload_date date;
  declare v_action_success_status tinyint default 0;
  declare v_inward_gid int default 0;
  declare v_days int default 45;
  
  declare done int default 0;

  DECLARE inward_csr cursor FOR
    select inward_gid from sta_trn_tinward
    where upload_gid = in_upload_gid
    and delete_flag = 'N';

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;

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
  END;

  
  if datediff(curdate(),in_approved_date) < 0 then
    set err_msg := concat(err_msg,'System will not accept future approved date,');
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

  
  select status_value into v_upload_done_status from sta_mst_tstatus
  where status_type = 'Upload'
  and status_desc = 'Upload Done'
  and delete_flag = 'N';

  set v_upload_done_status := ifnull(v_upload_done_status,0);

  if v_upload_done_status = 0 then
    set err_msg := concat(err_msg,'Upload done status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_upload_success_status from sta_mst_tstatus
  where status_type = 'Upload'
  and status_desc = 'Success'
  and delete_flag = 'N';

  set v_upload_success_status := ifnull(v_upload_success_status,0);

  if v_upload_success_status = 0 then
    set err_msg := concat(err_msg,'Upload success status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_upload_failure_status from sta_mst_tstatus
  where status_type = 'Upload'
  and status_desc = 'Failure'
  and delete_flag = 'N';

  set v_upload_failure_status := ifnull(v_upload_failure_status,0);

  if v_upload_failure_status = 0 then
    set err_msg := concat(err_msg,'Upload failure status to be maintained,');
    set err_flag := true;
  end if;

  
  select upload_gid,upload_date into v_upload_gid,v_upload_date from sta_trn_tupload
  where upload_gid = in_upload_gid
  and upload_status = v_upload_done_status
  and delete_flag = 'N';

  set v_upload_gid = ifnull(v_upload_gid,0);

  if v_upload_gid = 0 then
    set err_msg := concat(err_msg,'Upload status already updated,');
    set err_flag := true;
  end if;

  if datediff(in_approved_date,v_upload_date) < 0 then
    set err_msg := concat(err_msg,'Approved date is less than upload date,');
    set err_flag := true;
  end if;

  if err_flag = false then
    if in_upload_status = v_upload_success_status then
      
      update sta_trn_tupload set
        update_remark = in_remark,
        update_date = sysdate(),
        status_update_date = in_approved_date,
        upload_status = v_upload_success_status
      where upload_gid = in_upload_gid
      and upload_status = v_upload_done_status
      and delete_flag = 'N';

      update sta_trn_tinward set
        approved_date = in_approved_date 
      where upload_gid = in_upload_gid
      and approved_date is null
      and delete_flag = 'N';
      
      set done = 0;

      open inward_csr;

      inward_loop:loop
        fetch inward_csr into v_inward_gid;

        if done = 1 then
          leave inward_loop;
        end if;
        
        # Reset v_days to 45 for every inward_gid
		set v_days = 45;
		
        # LOC Reminder added on 13-05-2025 implemented by Mohan
        if exists (select '*' from sta_trn_tinward 
				   where inward_gid = v_inward_gid
				   and delete_flag = 'N' and reminder_flag = 'Y') then
			WHILE v_days <= 120 DO
			INSERT INTO sta_trn_tlocreminder (
				inward_gid, issued_date, days, status, insert_by, insert_date, delete_flag
			) VALUES (
				v_inward_gid,
				DATE_ADD(in_approved_date, INTERVAL v_days DAY),
				v_days,
				'Yet to Sent',
				in_action_by,
				NOW(),
				'N'
			);

			SET v_days = v_days + 45;
            if v_days = 135 then
				 set v_days = 120;
            end if;
            
			END WHILE;
        end if;
        
        # CA Allotment Added on 18-06-2024 implemented by Mohan
        if exists(select '*' from sta_trn_tinward 
				  where inward_gid = v_inward_gid
				  and tran_code = 'AM'
				  and delete_flag = 'N') then
                  
                  #Increase the Folio share on folio table
                  update sta_trn_tinward as a
                  inner join sta_trn_tfolio as b on a.folio_gid = b.folio_gid and b.delete_flag = 'N'
				  set b.folio_shares = b.folio_shares + a.share_count
				  where a.inward_gid = v_inward_gid
				  and a.tran_code = 'AM' and a.delete_flag = 'N';
                  
                  #Insert on Folio tran table
                  insert into sta_trn_tfoliotran (inward_gid,folio_gid,tran_date,tran_desc,
							  tran_count,tran_mode,mult,tran_remark,ref_folio_gid,delete_flag)
				  select inward_gid,folio_gid,curdate(),'CA Allotment',share_count,'C',1,'',0,'N'
                  from sta_trn_tinward 
                  where inward_gid = v_inward_gid
                  and tran_code = 'AM' and delete_flag = 'N';
                  
                  #Update the share qty
                  update sta_trn_tinward as a
                  inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid
                  and b.delete_flag = 'N'
				  set b.share_qty = (b.share_qty + a.share_count)
                  where a.inward_gid = v_inward_gid
                  and a.tran_code = 'AM' and a.delete_flag = 'N';
                  
				#Added on 03-09-2025 implemented by Mohan <<Start>>
                  select comp_gid into v_comp_gid
                  from sta_trn_tinward where inward_gid = v_inward_gid
                  and delete_flag = 'N';
                  
                  #update the share capital
                  update sta_mst_tcompany 
                  set share_captial = (share_qty * paid_up_value)
                  where comp_gid = v_comp_gid and delete_flag = 'N';
                  
                  #Move the shares to cert and certdist table
                  call pr_sta_run_caentry(v_inward_gid,v_comp_gid);
                 #Added on 03-09-2025 implemented by Mohan <<end>>
		end if;
        
        
        call pr_sta_set_queuemove(v_inward_gid,'Upload Success',v_action_success_status,in_action_by,@result,@msg);
      end loop inward_loop;

      close inward_csr;

      set out_msg = 'Upload successfully updated !';
    elseif in_upload_status = v_upload_failure_status then
      
      update sta_trn_tupload as a
      inner join sta_mst_tcompany as b on b.comp_gid = a.comp_gid and a.delete_flag = 'N'
      set
        a.upload_status = v_upload_failure_status,
        a.update_remark = in_remark,
        a.update_date = sysdate(),
        a.status_update_date = sysdate(),
        b.transfer_sno = if(a.transfer_count = 0,b.transfer_sno,a.transfer_start_sno),
        b.cert_sno = if(a.cert_count = 0,b.cert_sno,a.cert_start_sno),
        b.objx_sno = if(a.objx_count = 0,b.objx_sno,a.objx_start_sno)
      where a.upload_gid = in_upload_gid
      and a.upload_status = v_upload_done_status
      and a.delete_flag = 'N';

      
      update sta_trn_tinward as a
      inner join sta_trn_tcertsplitentry as b on b.inward_gid = a.inward_gid
      set
        a.upload_gid = 0,
        a.tran_cert_no = 0,
        b.new_cert_no = 0
      where a.upload_gid = in_upload_gid
      and a.delete_flag = 'N';

      update sta_trn_tinward as a
      set
        a.upload_gid = 0,
        a.tran_cert_no = 0,
        a.transfer_no = 0,
        a.objx_no = 0
      where a.upload_gid = in_upload_gid
      and a.delete_flag = 'N';

      set out_msg = 'Upload revoked !';
    else
      set out_result = 0;
      set out_msg = 'Invalid upload status updation,';
      leave me;
    end if;
  else
    set out_result = 0;
    set out_msg = err_msg;
    leave me;
  end if;

  set out_result = 1;
END$$
DELIMITER ;
