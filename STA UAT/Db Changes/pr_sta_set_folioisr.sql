DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_folioisr`(
  in in_inward_gid int,
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare v_folio_gid int default 0;
  declare v_Signature_gid int default 0;
  declare v_isr_sno int(11);
  declare v_comp_gid int(11);
  declare v_action_by varchar(64);
  
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;

    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;
  
  
  insert into sta_trn_tfolioentryold
  (folioentry_gid,folio_addr1,folio_addr2,folio_addr3,folio_city,folio_state,folio_country,folio_pincode,
   folio_contact_no,folio_mail_id,bank_name,bank_acc_no,bank_ifsc_code,bank_branch,bank_beneficiary,
   bank_acc_type,bank_addr,bank_micr_code,holder1_pan_no,holder2_pan_no,holder3_pan_no,nominee_addr1,
   nominee_addr2,nominee_addr3,nominee_city,nominee_state,nominee_country,nominee_pincode,
   nominee_reg_no,nominee_name,nominee_dob,nominee_fms_name,nominee_guardian,nominee_occupation,
   nominee_nationality,nominee_emailid,nominee_relationship)
  select d.folioentry_gid,c.folio_addr1,c.folio_addr2,c.folio_addr3,c.folio_city,c.folio_state,c.folio_country,c.folio_pincode,
		 c.folio_contact_no,c.folio_mail_id,c.bank_name,c.bank_acc_no,c.bank_ifsc_code,c.bank_branch,c.bank_beneficiary,
		 c.bank_acc_type,c.bank_branch_addr,c.bank_micr_code,c.holder1_pan_no,c.holder2_pan_no,c.holder3_pan_no,
         c.nominee_addr1,c.nominee_addr2,c.nominee_addr3,c.nominee_city,c.nominee_state,c.nominee_country,
		 c.nominee_pincode,c.nominee_reg_no,c.nominee_name,c.nominee_dob,c.nominee_fms_name,c.nominee_guardian,
         c.nominee_occupation,c.nominee_nationality,c.nominee_emailid,c.nominee_relationship
  from sta_trn_tinward as b
  inner join sta_trn_tfolio as c on c.folio_gid = b.folio_gid and c.delete_flag = 'N'
  inner join sta_trn_tfolioentry as d on d.inward_gid = b.inward_gid and d.delete_flag = 'N'
  where b.inward_gid = in_inward_gid
  and b.update_completed = 'Q'
  and b.chklst_disc = 0
  and b.delete_flag = 'N';

  update sta_trn_tfolio as a
  inner join sta_trn_tinward as b on b.folio_gid = a.folio_gid and b.delete_flag = 'N'
  inner join sta_trn_tfolioentry as c on c.inward_gid = b.inward_gid and c.delete_flag = 'N'
  set
    a.folio_addr1 = c.folio_addr1,
    a.folio_addr2 = c.folio_addr2,
    a.folio_addr3 = c.folio_addr3,
    a.folio_city = c.folio_city,
    a.folio_state = c.folio_state,
    a.folio_country = c.folio_country,
    a.folio_pincode = c.folio_pincode,
    a.folio_contact_no = c.folio_contact_no,
    a.folio_mail_id = c.folio_mail_id,
    a.bank_name = c.bank_name,
    a.bank_acc_no = c.bank_acc_no,
    a.bank_ifsc_code = c.bank_ifsc_code,
    a.bank_branch = c.bank_branch,
    a.bank_beneficiary = c.bank_beneficiary,
    a.bank_acc_type = c.bank_acc_type,
    a.bank_branch_addr = c.bank_addr,
    a.bank_micr_code = c.bank_micr_code,
    a.holder1_pan_no = c.holder1_pan_no,
    a.holder2_pan_no = c.holder2_pan_no,
    a.holder3_pan_no = c.holder3_pan_no,
    a.nominee_addr1 = c.nominee_addr1,
    a.nominee_addr2 = c.nominee_addr2,
    a.nominee_addr3 = c.nominee_addr3,
    a.nominee_city = c.nominee_city,
    a.nominee_state = c.nominee_state,
    a.nominee_country = c.nominee_country,
    a.nominee_pincode = c.nominee_pincode,
    a.nominee_reg_no = c.nominee_reg_no,
    a.nominee_name = c.nominee_name,
    a.nominee_dob = c.nominee_dob,
    a.nominee_fms_name = c.nominee_fms_name,
    a.nominee_guardian = c.nominee_guardian,
    a.nominee_occupation = c.nominee_occupation,
    a.nominee_nationality = c.nominee_nationality,
    a.nominee_emailid = c.nominee_emailid,
    a.nominee_relationship = c.nominee_relationship,
    b.update_completed = 'Y'
  where b.inward_gid = in_inward_gid
  and b.update_completed = 'Q'
  and b.chklst_disc = 0
  and a.delete_flag = 'N';
  
  -- Signature Queue updation --
  select folio_gid into v_folio_gid from sta_trn_tinward where inward_gid=in_inward_gid and delete_flag='N';
   set v_folio_gid=ifnull(v_folio_gid,0);
    
   select signature_gid into v_Signature_gid from sta_trn_tsignature where inward_gid=in_inward_gid and delete_flag='N';
   set v_Signature_gid=ifnull(v_Signature_gid,0);
       
    update sta_trn_tsignature set delete_flag='Y' where folio_gid = v_folio_gid and delete_flag = 'N';
  
	update sta_trn_tsignature as a 
	inner join sta_trn_tinward as b on a.inward_gid=b.inward_gid and b.delete_flag='N'
	SET
    a.folio_gid=b.folio_gid,
	b.update_completed = 'Y'
	where b.inward_gid = in_inward_gid
    and b.update_completed = 'Q'
    and b.chklst_disc = 0
    and a.delete_flag = 'N';
    
	 update sta_trn_tfolio set
	 signature_gid = v_Signature_gid
	 where folio_gid = v_folio_gid
	 and delete_flag = 'N';
     
      select comp_gid into v_comp_gid from sta_trn_tinward
      where  inward_gid = in_inward_gid
      and 	 delete_flag = 'N';
     
      select isr_sno into  v_isr_sno from sta_mst_tcompany
      where comp_gid = v_comp_gid and delete_flag='N';
      
      update sta_trn_tinward set isr_sno = v_isr_sno
      where  inward_gid = in_inward_gid
      and 	 delete_flag = 'N';
      
      #START Added By Mohan on 03-11-2025 (Undo Foliofreeze)
      if exists (select 1 from sta_trn_tqueue
				  where inward_gid = in_inward_gid 
				  and queue_from = 'C' 
				  and queue_to = 'D' 
				  and delete_flag = 'N') then
		  select queue_from_user into v_action_by 
		  from sta_trn_tqueue
		  where inward_gid = in_inward_gid 
		  and queue_from = 'C' 
		  and queue_to = 'D' 
		  and delete_flag = 'N'; 
		  
		  call pr_sta_undo_foliofreeze(in_inward_gid,v_comp_gid,v_folio_gid,v_action_by,'','');
      end if;
      #END Added By Mohan on 03-11-2025 (Undo Foliofreeze)
      
  set out_result = 1;
  set out_msg = 'Record updated successfully';
END$$
DELIMITER ;
