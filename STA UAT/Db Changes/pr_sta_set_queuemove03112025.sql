DELIMITER $$
-- drop procedure if exists pr_sta_set_queuemove;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_queuemove`(
  in in_inward_gid int,
  in in_remark varchar(255),
  in in_action_status tinyint,
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;

  declare v_action_success_status tinyint default 0;
  declare v_action_reject_status tinyint default 0;
  declare v_action_reprocess_status tinyint default 0;

  declare v_cert_active_status tinyint default 0;
  declare v_cert_inactive_status tinyint default 0;

  declare v_queue_gid int default 0;
  declare v_queue_action_status tinyint default 0;
  declare v_queue_from char(1);
  declare v_queue_to char(1);
  declare v_queue_current_completed_status char(1);
  declare v_queue_current char(1);
  declare v_queue_success char(1);
  declare v_queue_reject char(1);
  declare v_inward_inprocess_status tinyint default 0;
  declare v_inward_reject_status tinyint default 0;
  declare v_inward_inex_status tinyint default 0;
  declare v_inward_reprocess_status tinyint default 0;
  declare v_inward_completed_status tinyint default 0;
  declare v_inward_status tinyint default 0;
  declare v_inward_completed char(1);
  declare v_update_completed char(1);
  declare v_update_queue_code char(1);
  declare v_update_flag char(1) default 'N';
  declare v_auth_queue_code char(1);
  declare v_complete_queue_code char(1);
  declare v_completed_flag char(1) default 'N';
  declare v_tran_code char(2);
  declare v_chklst_disc int default 0;
  declare v_cert_flag char(1) default '';
  declare v_transfer_flag char(1) default '';
  declare v_upload_gid int default 0;
  declare v_comp_gid,v_folio_gid int(10);
  declare v_inward_foliofreeze_status int(10) default 128;
  
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;

    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;

  
  select
    c.queue_from,c.queue_to,d.completed_flag,b.auth_queue_code,b.complete_queue_code,a.tran_code,
    a.inward_completed,a.update_completed,b.update_queue_code,a.chklst_disc,b.cert_flag,b.transfer_flag,a.upload_gid
  into
    v_queue_from,v_queue_current,v_queue_current_completed_status,v_auth_queue_code,v_complete_queue_code,v_tran_code,
    v_inward_completed,v_update_completed,v_update_queue_code,v_chklst_disc,v_cert_flag,v_transfer_flag,v_upload_gid
  from sta_trn_tinward as a
  inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N'
  inner join sta_trn_tqueue as c on c.queue_gid = a.queue_gid and c.delete_flag = 'N'
  inner join sta_mst_tgroup as d on d.group_code = c.queue_to and d.delete_flag = 'N'
  where a.inward_gid = in_inward_gid
  and a.delete_flag = 'N';

  set v_queue_current = ifnull(v_queue_current,'');
  set v_auth_queue_code = ifnull(v_auth_queue_code,'');
  set v_complete_queue_code = ifnull(v_complete_queue_code,'');
  set v_upload_gid = ifnull(v_upload_gid,0);
  set v_inward_completed = ifnull(v_inward_completed,'');

  if v_inward_completed = 'Y' then
    set err_msg := concat(err_msg,'Inward completed,');
    set err_flag := true;
  end if;

  if v_auth_queue_code = '' then
    set err_msg := concat(err_msg,'Auth code not available in sta_mst_ttrantype,');
    set err_flag := true;
  end if;

  if v_complete_queue_code = '' then
    set err_msg := concat(err_msg,'Complete code not available in sta_mst_ttrantype,');
    set err_flag := true;
  end if;

  if v_queue_current = '' then
    set err_msg := concat(err_msg,'Current queue cannot be blank,');
    set err_flag := true;
  end if;

  select
    next_group_code,prev_group_code
  into
    v_queue_success,v_queue_reject from sta_mst_tgroup
  where group_code = v_queue_current
  and delete_flag = 'N';

  set v_queue_success = ifnull(v_queue_success,'');
  set v_queue_reject = ifnull(v_queue_reject,'');

  if v_queue_reject = '' then
    set v_queue_reject = v_queue_from;
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
    set err_msg := concat(err_msg,'Action success status to be maintained,');
    set err_flag := true;
  end if;

  select status_value into v_action_reprocess_status from sta_mst_tstatus
  where status_type = 'Action'
  and status_desc = 'Reprocess'
  and delete_flag = 'N';

  set v_action_reprocess_status := ifnull(v_action_reprocess_status,0);

  if v_action_reprocess_status = 0 then
    set err_msg := concat(err_msg,'Action reprocess status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_cert_active_status from sta_mst_tstatus
  where status_type = 'Certificate'
  and status_desc = 'Active'
  and delete_flag = 'N';

  set v_cert_active_status := ifnull(v_cert_active_status,0);

  if v_cert_active_status = 0 then
    set err_msg := concat(err_msg,'Certificate active status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_cert_inactive_status from sta_mst_tstatus
  where status_type = 'Certificate'
  and status_desc = 'Inactive'
  and delete_flag = 'N';

  set v_cert_inactive_status := ifnull(v_cert_inactive_status,0);

  if v_cert_inactive_status = 0 then
    set err_msg := concat(err_msg,'Certificate inactive status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_inward_inprocess_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Inprocess'
  and delete_flag = 'N';

  set v_inward_inprocess_status := ifnull(v_inward_inprocess_status,0);

  if v_inward_inprocess_status = 0 then
    set err_msg := concat(err_msg,'Inward inprocess status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_inward_reject_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Reject'
  and delete_flag = 'N';

  set v_inward_reject_status := ifnull(v_inward_reject_status,0);

  if v_inward_reject_status = 0 then
    set err_msg := concat(err_msg,'Inward reject status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_inward_reprocess_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Reprocess'
  and delete_flag = 'N';

  set v_inward_reprocess_status := ifnull(v_inward_reprocess_status,0);

  if v_inward_reprocess_status = 0 then
    set err_msg := concat(err_msg,'Inward reprocess status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_inward_inex_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Inex'
  and delete_flag = 'N';

  set v_inward_inex_status := ifnull(v_inward_inex_status,0);

  if v_inward_inex_status = 0 then
    set err_msg := concat(err_msg,'Inward inex status to be maintained,');
    set err_flag := true;
  end if;

  
  select status_value into v_inward_completed_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Completed'
  and delete_flag = 'N';

  set v_inward_completed_status := ifnull(v_inward_completed_status,0);

  if v_inward_completed_status = 0 then
    set err_msg := concat(err_msg,'Inward completed status to be maintained,');
    set err_flag := true;
  end if;

  
  if v_queue_reject = 'N' and in_action_status = v_action_reject_status then
    if not exists(select inward_gid from sta_trn_tinward
      where inward_gid = in_inward_gid
      and tran_folio_gid = 0
      and tran_cert_gid = 0
      and (select count(*) from sta_trn_tfolioentry where inward_gid = in_inward_gid and delete_flag = 'N') = 0
      and (select count(*) from sta_trn_tcertentry where inward_gid = in_inward_gid and delete_flag = 'N') = 0
      and (select count(*) from sta_trn_tcertsplitentry where inward_gid = in_inward_gid and delete_flag = 'N') = 0
      and delete_flag = 'N') then

      set err_msg := concat(err_msg,char(13),char(10),'Not allowed to move to inward,');
      set err_flag := true;
    end if;
  end if;

  
  if in_inward_gid = 0 then
    set err_msg := concat(err_msg,'Inward gid is zero,');
    set err_flag := true;
  end if;

  
  select queue_gid into v_queue_gid from sta_trn_tinward
  where inward_gid = in_inward_gid
  and delete_flag = 'N';

  set v_queue_gid := ifnull(v_queue_gid,0);

  if v_queue_gid = 0 then
    set err_msg := concat(err_msg,'Queue gid not found,');
    set err_flag := true;
  end if;

  
  select action_status into v_queue_action_status from sta_trn_tqueue
  where queue_gid = v_queue_gid
  and delete_flag = 'N';

  set v_queue_action_status := ifnull(v_queue_action_status,0);

  if v_queue_action_status > 0 then
    set err_msg := concat(err_msg,'Queue already updated,');
    set err_flag := true;
  end if;

  
  if err_flag = false then
    
    if in_action_status = v_action_success_status then
    
      set v_queue_to = v_queue_success;

      
      if v_update_queue_code = v_queue_current and v_chklst_disc = 0 then
        set v_update_flag = 'Y';
      else
        set v_update_flag = 'N';
      end if;

      if (v_complete_queue_code = v_queue_current) or (v_queue_current_completed_status = 'Y') then
        set v_completed_flag = 'Y';
        set v_inward_status = v_inward_completed_status;
      else
        set v_inward_status = v_inward_inprocess_status;
      end if;
    else
      set v_queue_to = v_queue_reject;

      if v_queue_current <> 'I' then
        set v_inward_status = v_inward_reject_status;
      else
        set v_inward_status = v_inward_reprocess_status;
      end if;
    end if;

    if v_completed_flag = 'Y'
      and v_queue_current <> 'D'
      and v_chklst_disc > 0 then
      
      set v_completed_flag = 'N';
      set v_update_flag = 'N';
      set v_queue_to = 'I';
      set v_inward_status = v_inward_inex_status;
    end if;

    
    if (v_cert_flag = 'Y' or v_transfer_flag = 'Y')
      and v_chklst_disc = 0
      and v_queue_current <> 'D'
      and in_action_status = v_action_success_status then
      if exists(select a.cert_gid from sta_trn_tcertentry as a
        inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N'
        where a.inward_gid = in_inward_gid
        and ((b.cert_status <> v_cert_active_status) or (curdate() between b.lockin_period_from and b.lockin_period_to))
        and a.delete_flag = 'N') then

        set err_msg := concat(err_msg,'Trying to transact invalid certificate,');
        set err_flag := true;
      end if;
    elseif (v_tran_code = 'CL' or v_tran_code = 'RL')
      and v_chklst_disc = 0
      and v_queue_current <> 'D' 
      and in_action_status = v_action_success_status then
      if exists(select a.cert_gid from sta_trn_tcertentry as a
        inner join sta_trn_tcert as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N'
        where a.inward_gid = in_inward_gid
        and b.cert_status = v_cert_inactive_status
        and a.delete_flag = 'N') then

        set err_msg := concat(err_msg,'Trying to transact invalid certificate,');
        set err_flag := true;
      end if;
    end if;
  end if;

  if v_tran_code = 'OT' then
     
     set v_completed_flag = 'N';
     set v_update_flag = 'N';
     set v_queue_to = 'I';
     set v_inward_status = v_inward_inex_status;
  end if;

  if err_flag = false then
    
    if v_queue_current = 'M' and v_chklst_disc > 0 then
      set v_completed_flag = 'N';
      set v_queue_to = 'I';
      set v_inward_status = v_inward_inex_status;
    end if;

    
    update sta_trn_tinward set
      inward_completed = v_completed_flag,
      inward_status = v_inward_status,
      inward_all_status = inward_all_status | v_inward_status
    where inward_gid = in_inward_gid
    and delete_flag = 'N';

    
    update sta_trn_tqueue set
      action_status = in_action_status,
      action_remark = in_remark,
      action_date = sysdate(),
      action_by = in_action_by
    where queue_gid = v_queue_gid
    and action_status = 0
    and delete_flag = 'N';
    
    #START Added by Mohan 03-11-2025 (validate foliofreeze status and move from Maker to Inex)
    if exists (select 1 from sta_mst_ttrantype 
			   where trantype_code = v_tran_code
			   and foliofreeze_flag = 'Y'
			   and delete_flag = 'N') and v_queue_current = 'M' then 
               
		#Get comp_gid,folio_gid
        select comp_gid, folio_gid 
		into v_comp_gid, v_folio_gid
        from sta_trn_tinward
        where inward_gid = in_inward_gid
        and delete_flag = 'N';
        
		call pr_sta_validate_foliofreeze(in_inward_gid,v_comp_gid,v_folio_gid,v_tran_code,
		'',@out_result,@out_msg);
        
        if @out_result = 0 then
			set v_queue_to = 'I';
			 
			 # update folio_remark and inward_all_status against inward 
			 update sta_trn_tinward 
			 set inward_all_status = inward_all_status | v_inward_foliofreeze_status,
				 folio_remark = concat(ifnull(folio_remark,''),' FolioFreezed,')
			 where inward_gid = in_inward_gid
			 and delete_flag = 'N';
        end if;
	elseif v_queue_current = 'I' and in_action_status = v_action_reprocess_status then
		 # update folio_remark and inward_all_status against inward 
		 update sta_trn_tinward 
		 set inward_all_status = (inward_all_status | v_inward_foliofreeze_status) ^ v_inward_foliofreeze_status,
			 folio_remark = replace(folio_remark,'FolioFreezed,','')
		 where inward_gid = in_inward_gid
		 and delete_flag = 'N';
    end if;
    #END Added by Mohan 03-11-2025 (validate foliofreeze status and move from Maker to Inex)
    
    if v_completed_flag = 'N' then
      call pr_sta_ins_queue(in_inward_gid,in_action_by,v_queue_current,v_queue_to,'',@queue_gid,@out,@msg);
      set out_msg = @msg;
    end if;

    
    if v_update_completed = 'N' and v_update_flag = 'Y' then
      set @msg = 'Action failed !';
      
      update sta_trn_tinward set
          update_completed = 'Q'
      where inward_gid = in_inward_gid
      and delete_flag = 'N';

      
      if v_tran_code = 'CA' then
        call pr_sta_set_folioaddress(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CP' then
        call pr_sta_set_foliopan(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'NU' then
        call pr_sta_set_folionominee(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CC' then
        call pr_sta_set_foliocategory(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CB' then
        call pr_sta_set_foliobank(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'NC' then
        call pr_sta_set_folioholder(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'ST' then
        call pr_sta_set_cert_stoptransfer(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CL' then
        call pr_sta_set_certlock(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'RL' then
        call pr_sta_set_certreleaselock(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'RT' then
        call pr_sta_set_cert_releasetransfer(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'TR' or v_tran_code = 'TM' or v_tran_code = 'TP' or v_tran_code = 'DM' or v_tran_code = 'FC'
          or v_tran_code = 'DT' or v_tran_code = 'OL' then
        call pr_sta_set_certtran(in_inward_gid,@result,@msg);
        -- Added by Mohan on 20-05-2025 After demated Loc reminder No need to send
        call pr_sta_set_locreminderdemated(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'SP' then
        call pr_sta_set_certsplit(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CO' or v_tran_code = 'LS' or v_tran_code = 'RM' or v_tran_code = 'DP' then
        call pr_sta_set_certissue(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'CS' then
        call pr_sta_set_certdepsplit(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'DS' then
        call pr_sta_set_certdistdepsplit(in_inward_gid,@result,@msg);
      end if;

      
      if v_tran_code = 'DC' then
        call pr_sta_set_certdepconsolidation(in_inward_gid,@result,@msg);
      end if;
      
      
       if v_tran_code = 'SG' then
        call pr_sta_set_signaturequeue(in_inward_gid,@result,@msg);
      end if;
      
      if v_tran_code = 'IS' then
        call pr_sta_set_folioisr(in_inward_gid,@result,@msg);
      end if;
	
      if v_tran_code = 'IE' then
		call pr_set_iepfclaims(in_inward_gid,@result,@msg);
      end if;
      
      set out_msg = @msg;
    end if;
  else
    set out_result = 0;
    set out_msg = err_msg;
    leave me;
  end if;

  set out_result = 1;
END$$
DELIMITER ;
