DELIMITER $$
-- drop procedure if exists pr_sta_mst_companysubgroup;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_mst_companysubgroup`(
in in_compsubgrp_gid int,
in in_compgrp_gid int,
in in_compsubgrp_code varchar(32),
in in_compsubgrp_name varchar(225),
in in_action varchar(16),
in in_action_by varchar(125),
out out_result int,
out out_msg text)
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
  
  if in_compgrp_gid <= 0 then
	set err_msg := concat(err_msg,'Company Group cannot be blank,');
    set err_flag := true;
  end if;
    
  if in_compsubgrp_code = '' then
	set err_msg := concat(err_msg,'Company Sub Group Code cannot be blank,');
    set err_flag := true;
  end if;
  
   if in_compsubgrp_name = '' then
	set err_msg := concat(err_msg,'Company Sub Group Name cannot be blank,');
    set err_flag := true;
  end if;
  
  if in_action = 'INSERT' then
	if err_flag = false then
		if exists(select in_compsubgrp_code from sta_mst_tcompanysubgroup
				  where compsubgrp_code = in_compsubgrp_code
				  and delete_flag = 'N') then
			set err_msg := concat(err_msg,'Company Sub Group Code already exists');
			set err_flag := true;
		end if;
        
		if exists(select in_compsubgrp_name from sta_mst_tcompanysubgroup
				  where compsubgrp_name = in_compsubgrp_name
				  and delete_flag = 'N') then
			set err_msg := concat(err_msg,'Company Sub Group Name already exists');
			set err_flag := true;
		end if;
   end if;
      
   if err_flag= false then
     START TRANSACTION;
     insert into sta_mst_tcompanysubgroup(compgrp_gid,compsubgrp_code,compsubgrp_name)
     values(in_compgrp_gid,in_compsubgrp_code,in_compsubgrp_name);
     commit;
	set out_result = 1;
    set out_msg = 'Record inserted successfully';
    leave me;
  else     
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
   end if;   
  end if;
  
  if in_action = 'UPDATE' then
   if err_flag = false then
		if in_compsubgrp_gid = 0 then
		 set err_msg := concat(err_msg,'Please Select the record');
		 set err_flag := true;
	   end if;
   
		-- Check for duplicate Company SUB Group Code
		if (select count(*) 
			from sta_mst_tcompanysubgroup
			where compsubgrp_code = in_compsubgrp_code
            and compgrp_gid = in_compgrp_gid
			and delete_flag = 'N'
			and compsubgrp_gid != in_compsubgrp_gid) > 0 then
			set err_msg := concat(err_msg, 'Company Sub Group Code cannot be duplicate. ');
			set err_flag := true;
		end if;

		-- Check for duplicate Company SUB Group Name
		if (select count(*) 
			from sta_mst_tcompanysubgroup
			where compsubgrp_name = in_compsubgrp_name
            and compgrp_gid = in_compgrp_gid
			and delete_flag = 'N'
			and compsubgrp_gid != in_compsubgrp_gid) > 0 then
			set err_msg := concat(err_msg, 'Company Sub Group Name cannot be duplicate. ');
			set err_flag := true;
		end if;
	   
		if err_flag= false then
			 START TRANSACTION;
			 update sta_mst_tcompanysubgroup set
					 compgrp_gid = in_compgrp_gid,
					 compsubgrp_name = in_compsubgrp_name
			 where compsubgrp_gid = in_compgrp_gid;
			 commit;
				set out_result = 1;
				set out_msg = 'Record Updated successfully';
			leave me;
		else     
			  set out_result = 0;
			  set out_msg = err_msg;
			  leave me;
		end if;    
	end if;
end if;
END$$
DELIMITER ;
