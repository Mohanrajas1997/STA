DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_mst_tcompany_new`(
  in in_comp_gid int,
  in in_compgrp_gid int,
  in in_compsubgrp_gid int,
  in in_entity_gid int,
  in in_comp_code varchar(16),
  in in_comp_short_code varchar(8),
  in in_comp_name varchar(64),
  in in_isin_id varchar(16),
  in in_folio_no_format varchar(16),
  in in_folio_prefix_flag char(1),
  in in_electronics_flag char(1),
  in in_folio_prefix_sno_flag char(1),
  in in_folio_prefix varchar(8),
  in in_folio_prefix_field varchar(16),
  in in_upload_sno int(11),
  in in_folio_sno int(11),
  in in_transfer_sno int(11),
  in in_cert_sno int(11),
  in in_objx_sno int(11),
  in in_inward_sno int(11),
  in in_comp_listed char(1),
  in in_active_flag char(1),
  in in_share_captial double(15,2),
  in in_security_type varchar(45),
  in in_share_qty double(15,2),
  in in_paid_up_value int(11),
  in in_address1 varchar(128),
  in in_address2 varchar(128),
  in in_address3 varchar(128),
  in in_city varchar(64),
  in in_state varchar(64),
  in in_country varchar(64),
  in in_pincode varchar(64),
  in in_cin_no varchar(64),
  in in_pan_no varchar(64),
  in in_contact_person varchar(128),
  in in_contact_no varchar(128),
  in in_email_id varchar(45),
  in in_start_date date,
  in in_maturity_date date,
  in in_depository_code varchar(32),
  in in_action varchar(16),
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare v_folio_prefix_length int(11) default 0;

  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  /*BEGIN
    ROLLBACK;

    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;*/

  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;

  IF in_action = "SELECT" THEN
    set in_comp_gid := if(in_comp_gid = 0,null,in_comp_gid);
    set in_comp_name := if(in_comp_name = '',null,in_comp_name);

    SELECT * FROM sta_mst_tcompany
    WHERE comp_gid = ifnull(in_comp_gid,comp_gid)
    and  comp_name = ifnull(in_comp_name,comp_name)
    and delete_flag = 'N';
  END IF;
  
  if not exists (select '*' from sta_mst_tcompanygroup
				  where compgrp_gid = in_compgrp_gid
				  and delete_flag = 'N') then
	set err_msg := concat(err_msg,'Invalid Company Group,');
    set err_flag := true;
  end if;
  
  if not exists (select '*' from sta_mst_tcompanysubgroup
				  where compgrp_gid = in_compgrp_gid
                  and compsubgrp_gid = in_compsubgrp_gid
				  and delete_flag = 'N') then
	set err_msg := concat(err_msg,'Invalid Company SubGroup,');
    set err_flag := true;
  end if;
  
  /*if in_compgrp_gid <= 0 then
	set err_msg := concat(err_msg,'Blank Company group name,');
    set err_flag := true;
  end if;
  
  if in_compsubgrp_gid <= 0 then
	set err_msg := concat(err_msg,'Blank Company Sub group name,');
    set err_flag := true;
  end if;*/

  if in_comp_code = '' then
    set err_msg := concat(err_msg,'Blank Company Code,');
    set err_flag := true;
  end if;

  if in_comp_name = '' then
    set err_msg := concat(err_msg,'Blank Company Name,');
    set err_flag := true;
  end if;
  
  if in_comp_short_code = '' then
    set err_msg := concat(err_msg,'Blank Company Short Code,');
    set err_flag := true;
  end if;
  
  if in_isin_id = '' then
    set err_msg := concat(err_msg,'Blank Isin Id,');
    set err_flag := true;
  end if;
  
  if in_folio_no_format = '' then
    set err_msg := concat(err_msg,'Blank Folio No Format,');
    set err_flag := true;
  end if;
  
  if in_folio_prefix_flag = '' then
    set err_msg := concat(err_msg,'Blank Folio Prefix Flag,');
    set err_flag := true;
  end if;
  
  if in_folio_prefix_sno_flag = '' then
    set err_msg := concat(err_msg,'Blank Folio Prefix Sno Flag,');
    set err_flag := true;
  end if;
  
  if in_folio_prefix_flag = 'Y' and in_folio_prefix = '' then
    set err_msg := concat(err_msg,'Blank Folio Prefix,');
    set err_flag := true;
  end if;
  
  if in_folio_prefix <> '' then
	select length(in_folio_prefix) into v_folio_prefix_length;
  end if;
  
  if in_folio_prefix_sno_flag = 'Y' and in_folio_prefix_field = '' then
    set err_msg := concat(err_msg,'Blank Folio Prefix field,');
    set err_flag := true;
  end if;
  
  if in_folio_prefix_sno_flag = 'Y' and in_folio_prefix_field = '' then
    set err_msg := concat(err_msg,'Blank Folio Prefix field,');
    set err_flag := true;
  end if;
  
  if in_upload_sno <= 0 then
    set err_msg := concat(err_msg,'Upload Sno should be greater than Zero,');
    set err_flag := true;
  end if;

  if in_folio_sno <= 0 then
    set err_msg := concat(err_msg,'Folio Sno should be greater than Zero,');
    set err_flag := true;
  end if;
  
  if in_transfer_sno <= 0 then
    set err_msg := concat(err_msg,'Transfer Sno should be greater than Zero,');
    set err_flag := true;
  end if;
  
  if in_cert_sno <= 0 then
    set err_msg := concat(err_msg,'Certificate Sno should be greater than Zero,');
    set err_flag := true;
  end if;
  
  if in_objx_sno <= 0 then
    set err_msg := concat(err_msg,'Objx Sno should be greater than Zero,');
    set err_flag := true;
  end if;
  
  if in_inward_sno <= 0 then
    set err_msg := concat(err_msg,'Inward Sno should be greater than Zero,');
    set err_flag := true;
  end if;
  
  if in_comp_listed = '' then
    set err_msg := concat(err_msg,'Blank Company listed,');
    set err_flag := true;
  end if;
  
  if in_active_flag = '' then
    set err_msg := concat(err_msg,'Blank Active Flag,');
    set err_flag := true;
  end if;
  
  if not exists (select '*' from sta_mst_tdepository 
				  where depository_name = in_depository_code
				  and delete_flag = 'N') then
	set err_msg := concat(err_msg,'Invalid Depository Type,');
    set err_flag := true;
  end if;
  
  
  if not exists (select '*' from sta_mst_tsecuritytype 
				  where securitytype_code = in_security_type
				  and delete_flag = 'N') then
	set err_msg := concat(err_msg,'Invalid Security Type,');
    set err_flag := true;
  end if;
  
  if in_maturity_date = "0001-01-01" then
	set in_maturity_date = null;
  end if;

  IF in_action = "INSERT" THEN
		#Duplicate Company Code
	   if exists(select comp_gid from sta_mst_tcompany where comp_code = in_comp_code and delete_flag = 'N') then
		  set err_msg := concat(err_msg,'Duplicate Company Code,');
		  set err_flag := true;
	   end if;
        #Duplicate Company Short Code
       if exists(select comp_gid from sta_mst_tcompany where comp_short_code = in_comp_short_code and delete_flag = 'N') then
		  set err_msg := concat(err_msg,'Duplicate Company Short Code,');
		  set err_flag := true;
	   end if;
        #Duplicate Isin Id
	   if exists(select comp_gid from sta_mst_tcompany where isin_id = in_isin_id and delete_flag = 'N') then
		  set err_msg := concat(err_msg,'Duplicate Isin Id,');
		  set err_flag := true;
	   end if;

    if err_flag = false then
	    START TRANSACTION;
		  INSERT INTO sta_mst_tcompany 
				 (entity_gid,compgrp_gid,compsubgrp_gid,comp_code,comp_short_code,comp_name,isin_id,folio_no_format,folio_prefix_flag,electronics_flag,
                 folio_prefix_sno_flag,folio_prefix,folio_prefix_field,folio_prefix_length,upload_sno,
                 folio_sno,transfer_sno,cert_sno,objx_sno,cdsl_sno,nsdl_sno,inward_sno,comp_listed,active_flag,
                 share_captial,share_type,share_qty,paid_up_value,address1,address2,address3,city,state,
                 country,pincode,cin_no,pan_no,depository_code,
                 contact_person,contact_no,email_id,start_date,maturity_date,delete_flag)
		  VALUES (1,in_compgrp_gid,in_compsubgrp_gid,in_comp_code,in_comp_short_code,in_comp_name,in_isin_id,in_folio_no_format,in_folio_prefix_flag,in_electronics_flag,
          in_folio_prefix_sno_flag,in_folio_prefix,in_folio_prefix_field,v_folio_prefix_length,in_upload_sno,
          in_folio_sno,in_transfer_sno,in_cert_sno,in_objx_sno,1,1,in_inward_sno,in_comp_listed,in_active_flag,
          in_share_captial,in_security_type,in_share_qty,in_paid_up_value,in_address1,in_address2,in_address3,
          in_city,in_state,in_country,in_pincode,in_cin_no,in_pan_no,in_depository_code,
          in_contact_person,in_contact_no,in_email_id,in_start_date,in_maturity_date,'N');
		COMMIT;
      set out_msg = "Record inserted successfully";
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;
  END IF;

  IF in_action = "UPDATE" THEN
	  if in_comp_gid = 0 then
		  set err_msg := concat(err_msg,'Blank Company gid,');
		  set err_flag := true;
	  end if;

	  if exists(select comp_gid from sta_mst_tcompany
              where comp_code = in_comp_code
              and comp_gid <> in_comp_gid
              and delete_flag = 'N') and in_comp_gid > 0 then
		  set err_msg := concat(err_msg,'Duplicate Company Code,');
		  set err_flag := true;
	  end if;
      
	 if exists(select comp_gid from sta_mst_tcompany
              where comp_short_code = in_comp_short_code
              and comp_gid <> in_comp_gid
              and delete_flag = 'N') and in_comp_gid > 0 then
		  set err_msg := concat(err_msg,'Duplicate Company Short Code,');
		  set err_flag := true;
	  end if;
      
	 if exists(select comp_gid from sta_mst_tcompany
              where comp_name = in_comp_name
              and comp_gid <> in_comp_gid
              and delete_flag = 'N') and in_comp_gid > 0 then
		  set err_msg := concat(err_msg,'Duplicate Company Name Code,');
		  set err_flag := true;
	  end if;
      
	 if exists(select comp_gid from sta_mst_tcompany
              where isin_id = in_isin_id
              and comp_gid <> in_comp_gid
              and delete_flag = 'N') and in_comp_gid > 0 then
		  set err_msg := concat(err_msg,'Duplicate Isin Id,');
		  set err_flag := true;
	  end if;

	  if err_flag = false then
      START TRANSACTION;
		  UPDATE sta_mst_tcompany set
			compgrp_gid = in_compgrp_gid,
            compsubgrp_gid = in_compsubgrp_gid,
			comp_code = in_comp_code,
			comp_short_code = in_comp_short_code,
			#comp_name = in_comp_name,
			#isin_id = in_isin_id,
			folio_no_format = in_folio_no_format,
			folio_prefix_flag = in_folio_prefix_flag,
            electronics_flag = in_electronics_flag,
			folio_prefix_sno_flag = in_folio_prefix_sno_flag,
			folio_prefix = in_folio_prefix,
			-- folio_prefix_field = in_folio_prefix_field,
			-- folio_prefix_length = v_folio_prefix_length,
			-- upload_sno = in_upload_sno,
			-- folio_sno = in_folio_sno,
			-- transfer_sno = in_transfer_sno,
			-- cert_sno = in_cert_sno,
			-- objx_sno = in_objx_sno,
			-- inward_sno = in_inward_sno,
			comp_listed  = in_comp_listed,
			active_flag = in_active_flag,
			share_captial = in_share_captial,
            address1 = in_address1,
            address2 = in_address2,
            address3 = in_address3,
            city = in_city,
            state = in_state,
            country = in_country,
            pincode = in_pincode,
            #share_type = in_security_type,
            share_qty = in_share_qty,
            paid_up_value = in_paid_up_value,
			#cin_no = in_cin_no,
			pan_no = in_pan_no,
            contact_person = in_contact_person,
            contact_no = in_contact_no,
            email_id = in_email_id,
            start_date = in_start_date,
            maturity_date = in_maturity_date,
            #depository_code = in_depository_code
            entry_mode = 'Edit'
		  WHERE comp_gid = in_comp_gid
		  and delete_flag = 'N';
      COMMIT;
      set out_msg = "Record updated successfully";
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;
  END IF;

  IF in_action = "DELETE" THEN
    START TRANSACTION;
		UPDATE sta_mst_tcompany set
		  delete_flag = 'Y'
		WHERE comp_gid = in_comp_gid
		and delete_flag = 'N';
    COMMIT;
    set out_msg = "Record deleted successfully";
  END IF;

  set out_result = 1;
  END$$
DELIMITER ;
