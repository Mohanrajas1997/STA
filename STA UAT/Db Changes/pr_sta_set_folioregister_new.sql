DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_folioregister_new`(
  in in_comp_gid int,
  in in_holder1_name varchar(128),
  in in_holder1_fh_name varchar(128),
  in in_holder1_pan_no varchar(16),
  in in_holder2_name varchar(128),
  in in_holder2_fh_name varchar(128),
  in in_holder2_pan_no varchar(16),
  in in_holder3_name varchar(128),
  in in_holder3_fh_name varchar(128),
  in in_holder3_pan_no varchar(16),
  in in_folio_addr1 varchar(64),
  in in_folio_addr2 varchar(64),
  in in_folio_addr3 varchar(64),
  in in_folio_city varchar(64),
  in in_folio_state varchar(64),
  in in_folio_country varchar(64),
  in in_folio_pincode varchar(16),
  in in_folio_mail_id varchar(128),
  in in_folio_contact_no varchar(128),
  in in_folio_category_gid int,
  
  in in_bank_name varchar(128),
  in in_bank_acc_no varchar(64),
  in in_bank_ifsc_code varchar(32),
  in in_bank_branch varchar(128),
  in in_bank_beneficiary varchar(128),
  in in_bank_acc_type char(1),
  in in_bank_addr varchar(128),
  in in_bank_micr_code varchar(16),
  in in_nominee_assign_flag char(1),
  in in_nominee_name varchar(128),
  in in_nominee_dob date,
  in in_nominee_fms_name varchar(128),
  in in_nominee_guardian varchar(128),
  in in_nominee_occupation varchar(128),
  in in_nominee_nationality varchar(128),
  in in_nominee_emailid varchar(128),
  in in_nominee_relationship varchar(128),
  in in_nominee_addr1 varchar(64),
  in in_nominee_addr2 varchar(64),
  in in_nominee_addr3 varchar(64),
  in in_nominee_city varchar(64),
  in in_nominee_state varchar(64),
  in in_nominee_country varchar(64),
  in in_nominee_pincode varchar(16),
  
  in in_action_by varchar(16),
  out out_folio_gid int,
  out out_msg text,
  out out_result int
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag varchar(10) default false;
  declare v_comp_gid int default 0;
  declare v_folio_gid int default 0;
  declare v_folio_sno int default 0;
  declare v_folio_no varchar(16) default '';
  declare v_folio_prefix_sno_flag char(1) default '';
  declare v_ret int default 0;

  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;

  
  
  set in_folio_category_gid = ifnull(in_folio_category_gid,0);

  
  select comp_gid,folio_prefix_sno_flag into v_comp_gid,v_folio_prefix_sno_flag from sta_mst_tcompany
  where comp_gid = in_comp_gid
  and delete_flag = 'N';

  set v_comp_gid = ifnull(v_comp_gid,0);

  if v_comp_gid = 0 then
    set err_msg := concat(err_msg,'Invalid company,');
    set err_flag := true;
  end if;

  if in_holder1_name = '' then
    set err_msg := concat(err_msg,'Holder1 name cannot be blank,');
    set err_flag := true;
  end if;

  if in_holder1_fh_name = '' then
    set err_msg := concat(err_msg,'Holder1 f/h name cannot be blank,');
    set err_flag := true;
  end if;

  if in_holder1_pan_no <> '' then
    select fn_sta_chk_panno(in_holder1_pan_no) into v_ret;

    if v_ret = 0 then
		  set err_msg := concat(err_msg,'Invalid holder1 pan no,');
		  set err_flag := true;
      #changes done by mohan on 28-05-2024  
	  else
			if exists (select '*' from sta_mst_tpanmaster 
					   where pan_no = in_holder1_pan_no
					   and debarrt_flag = 'Y'
					   and delete_flag = 'N') then
				set err_msg := concat(err_msg,'Holder1 pan no is Debarrt Pan,');
				set err_flag := true; 
			end if;
	  end if;
  end if;

  if in_holder2_pan_no <> '' then
    select fn_sta_chk_panno(in_holder2_pan_no) into v_ret;

    if v_ret = 0 then
      set err_msg := concat(err_msg,'Invalid holder2 pan no,');
      set err_flag := true;
	  #changes done by mohan on 28-05-2024  
	else
		if exists (select '*' from sta_mst_tpanmaster 
			   where pan_no = in_holder2_pan_no
			   and debarrt_flag = 'Y'
			   and delete_flag = 'N') then
		set err_msg := concat(err_msg,'Holder2 pan no is Debarrt Pan,');
		set err_flag := true;  
		end if;
   end if;
  end if;

  if in_holder3_pan_no <> '' then
    select fn_sta_chk_panno(in_holder3_pan_no) into v_ret;

    if v_ret = 0 then
		  set err_msg := concat(err_msg,'Invalid holder3 pan no,');
		  set err_flag := true;
      #changes done by mohan on 28-05-2024  
	  else
		if exists (select '*' from sta_mst_tpanmaster 
					   where pan_no = in_holder3_pan_no
					   and debarrt_flag = 'Y'
					   and delete_flag = 'N') then
			set err_msg := concat(err_msg,'Holder3 pan no is Debarrt Pan,');
			set err_flag := true;  
		end if;
	  end if;
  end if;

  if in_holder2_name <> '' and in_holder2_fh_name = '' then
    set err_msg := concat(err_msg,'Holder2 f/h name cannot be blank,');
    set err_flag := true;
  end if;

  if in_holder3_name <> '' and in_holder3_fh_name = '' then
    set err_msg := concat(err_msg,'Holder3 f/h name cannot be blank,');
    set err_flag := true;
  end if;

  if in_folio_addr1 = '' then
    set err_msg := concat(err_msg,'Addr1 cannot be blank,');
    set err_flag := true;
  end if;

  if in_folio_city = '' then
    set err_msg := concat(err_msg,'City cannot be blank,');
    set err_flag := true;
  end if;
  
   if in_folio_contact_no = '' then
    set err_msg := concat(err_msg,'Phone number cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_folio_pincode = '' then
    set err_msg := concat(err_msg,'Pincode cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_folio_mail_id = '' then
    set err_msg := concat(err_msg,'Email Id cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_bank_beneficiary = '' then
    set err_msg := concat(err_msg,'Beneficiary cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_bank_name = '' then
    set err_msg := concat(err_msg,'Bank cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_bank_ifsc_code = '' then
    set err_msg := concat(err_msg,'Ifsc code cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_bank_acc_type = '' then
    set err_msg := concat(err_msg,'A/C type cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_bank_acc_no = '' then
    set err_msg := concat(err_msg,'A/C no cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_nominee_assign_flag = '' then
	set err_msg := concat(err_msg,'please select nominee Yes/No,');
    set err_flag := true;
  end if;
  
  if in_nominee_assign_flag = 'Y' then
	  if in_nominee_name = '' then
		set err_msg := concat(err_msg,'Nominee name cannot be blank,');
		set err_flag := true;
	  end if;
		
	  if in_nominee_addr1 = '' then
		set err_msg := concat(err_msg,'Address1 cannot be blank,');
		set err_flag := true;
	  end if;

	  if in_nominee_city = '' then
		set err_msg := concat(err_msg,'City cannot be blank,');
		set err_flag := true;
	  end if;
  end if;
  
  if in_nominee_dob = '1900-01-01' then
	set in_nominee_dob := null;
  end if;

  if err_flag = false then
    if exists(select a.folio_gid from sta_trn_tfolio as a
      where a.comp_gid = v_comp_gid
      and a.holder1_name = in_holder1_name
      and a.holder1_fh_name = in_holder1_fh_name
      and a.holder1_pan_no = in_holder1_pan_no
      and a.holder2_name = in_holder2_name
      and a.holder2_fh_name = in_holder2_fh_name
      and a.holder2_pan_no = in_holder2_pan_no
      and a.holder3_name = in_holder3_name
      and a.holder3_fh_name = in_holder3_fh_name
      and a.holder3_pan_no = in_holder3_pan_no
      and a.folio_addr1 = in_folio_addr1
      and a.folio_addr2 = in_folio_addr2
      and a.folio_addr3 = in_folio_addr3
      and a.folio_city = in_folio_city
      and a.folio_state = in_folio_state
      and a.folio_country = in_folio_country
      and a.folio_pincode = in_folio_pincode
      and a.delete_flag = 'N') then

      set err_msg := concat(err_msg,'Folio alreay exists,');
      set err_flag := true;
    end if;
  end if;

  if err_flag = false then
    call pr_sta_generate_foliono(v_comp_gid,in_holder1_name,@folio_no,@folio_sno,@folio_prefix,@msg,@result);

    if @result = 0 then
      set err_msg := concat(err_msg,@msg);
      set err_flag := true;
    end if;
  end if;

  if err_flag = true then
    set out_result = 0;
    set out_msg = err_msg;

    leave me;
  end if;

  START TRANSACTION;
  INSERT INTO sta_trn_tfolio
  (
    comp_gid,
    folio_no,
    folio_sno,
    folio_no_prefix,
    holder1_name,
    holder1_fh_name,
    holder1_pan_no,
    holder2_name,
    holder2_fh_name,
    holder2_pan_no,
    holder3_name,
    holder3_fh_name,
    holder3_pan_no,
    folio_addr1,
    folio_addr2,
    folio_addr3,
    folio_city,
    folio_state,
    folio_country,
    folio_pincode,
    folio_mail_id,
    folio_contact_no,
    category_gid,
    bank_name,bank_acc_no,bank_ifsc_code,bank_branch,bank_beneficiary,
	bank_acc_type,bank_branch_addr,bank_micr_code,
	nominee_name,nominee_addr1,nominee_addr2,nominee_addr3,nominee_city,nominee_state,nominee_country,
	nominee_pincode,nominee_dob,nominee_fms_name,nominee_guardian,nominee_occupation,nominee_nationality,
	nominee_emailid,nominee_relationship,
    insert_date,
    insert_by
  ) VALUES
  (
    v_comp_gid,
    @folio_no,
    @folio_sno,
    @folio_prefix,
    in_holder1_name,
    in_holder1_fh_name,
    in_holder1_pan_no,
    in_holder2_name,
    in_holder2_fh_name,
    in_holder2_pan_no,
    in_holder3_name,
    in_holder3_fh_name,
    in_holder3_pan_no,
    in_folio_addr1,
    in_folio_addr2,
    in_folio_addr3,
    in_folio_city,
    in_folio_state,
    in_folio_country,
    in_folio_pincode,
    in_folio_mail_id,
    in_folio_contact_no,
    in_folio_category_gid,
    in_bank_name,in_bank_acc_no,in_bank_ifsc_code,in_bank_branch,in_bank_beneficiary,
	in_bank_acc_type,in_bank_addr,in_bank_micr_code,
	in_nominee_name,in_nominee_addr1,in_nominee_addr2,in_nominee_addr3,in_nominee_city,in_nominee_state,
	in_nominee_country,in_nominee_pincode,in_nominee_dob,in_nominee_fms_name,in_nominee_guardian,in_nominee_occupation,
	in_nominee_nationality,in_nominee_emailid,in_nominee_relationship,
    sysdate(),
    in_action_by
  );

  if v_folio_prefix_sno_flag <> 'Y' then
    update sta_mst_tcompany set folio_sno = folio_sno + 1 where comp_gid = v_comp_gid;
  end if;

  select max(folio_gid) into v_folio_gid from sta_trn_tfolio where comp_gid = v_comp_gid;

  COMMIT;

  set out_folio_gid = v_folio_gid;
  set out_result = 1;
  set out_msg = 'Folio registered successfully !';
 END$$
DELIMITER ;
