DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_mst_tdepositoryparticipant`(
  in in_dp_gid int,
  in in_dp_id int,
  in in_dp_name varchar(255),
  in in_depository_code varchar(32),
  in in_dp_address1 varchar(255),
  in in_dp_address2 varchar(255),
  in in_dp_address3 varchar(255),
  in in_dp_city varchar(64),
  in in_dp_state varchar(64),
  in in_dp_country varchar(64),
  in in_dp_pincode varchar(64),
  in in_dp_contact_no varchar(255),
  in in_dp_email_id varchar(45),
  in in_dp_profile_url varchar(255),
  in in_action varchar(16),
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;

  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;

  IF in_action = "SELECT" THEN
    set in_dp_gid := if(in_dp_gid = 0,null,in_dp_gid);
    set in_dp_id := if(in_dp_id = 0,null,in_dp_id);
    set in_dp_name := if(in_dp_name = '',null,in_dp_name);
    
    SELECT * FROM sta_mst_tdepositoryparticipant
    WHERE dp_gid = ifnull(in_dp_gid,dp_gid)
    and  dp_id = ifnull(in_dp_id,dp_id)
    and  dp_name = ifnull(in_dp_name,dp_name)
    and delete_flag = 'N';
  END IF;

  if in_dp_id = '' then
    set err_msg := concat(err_msg,'Blank DP ID,');
    set err_flag := true;
  end if;

  if in_dp_name = '' then
    set err_msg := concat(err_msg,'Blank DP Name,');
    set err_flag := true;
  end if;
  
  if not exists (select '*' from sta_mst_tdepository 
				  where depository_name = in_depository_code
				  and delete_flag = 'N') then
	set err_msg := concat(err_msg,'Invalid Depository Type,');
    set err_flag := true;
  end if;
  
  if in_dp_address1 = '' then
    set err_msg := concat(err_msg,'Blank Address1,');
    set err_flag := true;
  end if;
  
  if in_dp_city = '' then
    set err_msg := concat(err_msg,'Blank City,');
    set err_flag := true;
  end if;
  
  if in_dp_state = '' then
    set err_msg := concat(err_msg,'Blank State,');
    set err_flag := true;
  end if;
  
  if in_dp_pincode = '' then
    set err_msg := concat(err_msg,'Blank Pincode,');
    set err_flag := true;
  end if;
  
  IF in_action = "INSERT" THEN
		#Duplicate DP ID
	   if exists(select dp_gid from sta_mst_tdepositoryparticipant 
				 where dp_id = in_dp_id and dp_name = in_dp_name
                 and in_depository_code = in_depository_code and dp_pincode = in_dp_pincode
                 and delete_flag = 'N') then
		  set err_msg := concat(err_msg,'Duplicate Entry,');
		  set err_flag := true;
	   end if;

    if err_flag = false then
	    START TRANSACTION;
		  INSERT INTO sta_mst_tdepositoryparticipant 
				 (dp_id,dp_name,depository_code,dp_address1,dp_address2,dp_address3,
                  dp_city,dp_state,dp_country,dp_pincode,dp_contact_no,dp_email_id,dp_profile_url,
                  insert_date,insert_by,delete_flag)
		  VALUES (in_dp_id,in_dp_name,in_depository_code,in_dp_address1,in_dp_address2,in_dp_address3,
				  in_dp_city,in_dp_state,in_dp_country,in_dp_pincode,in_dp_contact_no,in_dp_email_id,in_dp_profile_url,
                  now(),in_action_by,'N');
		COMMIT;
      set out_msg = "Record inserted successfully";
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;
  END IF;

  IF in_action = "UPDATE" THEN
	  if in_dp_gid = 0 then
		  set err_msg := concat(err_msg,'Blank DP gid,');
		  set err_flag := true;
	  end if;

	  if exists(select dp_gid from sta_mst_tdepositoryparticipant
              where dp_id = in_dp_id and dp_name = in_dp_name
              and in_depository_code = in_depository_code and dp_pincode = in_dp_pincode
              and dp_gid <> in_dp_gid and delete_flag = 'N') and in_dp_gid > 0 then
		  set err_msg := concat(err_msg,'Duplicate Entry,');
		  set err_flag := true;
	  end if;
      
	  if err_flag = false then
      START TRANSACTION;
		  UPDATE sta_mst_tdepositoryparticipant set
			dp_id = in_dp_id,
			dp_name = in_dp_name,
            depository_code = in_depository_code,
            dp_address1 = in_dp_address1,
            dp_address2 = in_dp_address2,
            dp_address3 = in_dp_address3,
            dp_contact_no = in_dp_contact_no,
            dp_email_id = in_dp_email_id,
            dp_city = in_dp_city,
            dp_state = in_dp_state,
            dp_country = in_dp_country,
            dp_pincode = in_dp_pincode,
            dp_profile_url = in_dp_profile_url,
            update_date = now(),
            update_by = in_action_by
		  WHERE dp_gid = in_dp_gid
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
		UPDATE sta_mst_tdepositoryparticipant set
		  delete_flag = 'Y'
		WHERE dp_gid = in_dp_gid
		and delete_flag = 'N';
    COMMIT;
    set out_msg = "Record deleted successfully";
  END IF;

  set out_result = 1;
  END$$
DELIMITER ;
