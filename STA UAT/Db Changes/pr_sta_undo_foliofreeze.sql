DELIMITER $$
-- drop procedure if exists pr_sta_undo_foliofreeze;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_undo_foliofreeze`(in in_inward_gid int,
  in in_comp_gid int,
  in in_folio_gid int,
  in in_action_by varchar(64),
  out out_result int,
  out out_msg text)
BEGIN
	DECLARE err_msg TEXT DEFAULT '';
    DECLARE err_flag VARCHAR(10) DEFAULT 'false';
    declare done int default 0;
    -- declare v_inward_status tinyint default 0;
    -- declare v_inward_foliofreeze_released_status int(10) default 0;
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
			set out_result = 0;
			set out_msg = 'SQLEXCEPTION';
	END;
 
	 set in_inward_gid = ifnull(in_inward_gid,0);
	 set in_comp_gid = ifnull(in_comp_gid,0);
	 set in_folio_gid = ifnull(in_folio_gid,0);
 
	/* select status_value into v_inward_foliofreeze_released_status from sta_mst_tstatus
	 where status_type = 'Inward'
	 and status_desc = 'Foliofreeze Released'
	 and delete_flag = 'N';

	 set v_inward_foliofreeze_released_status := ifnull(v_inward_foliofreeze_released_status,0);

  if v_inward_foliofreeze_released_status = 0 then
    set err_msg := concat(err_msg,'Inward foliofreeze released status to be maintained,');
    set err_flag := true;
  end if;*/

# FolioFreeze Undo
	-- if err_flag = false then
		if exists (select 1 from sta_trn_tfoliofreeze
				   where  comp_gid = in_comp_gid 
				   and folio_gid = in_folio_gid
				   and foliofreeze_flag = 'Y' and delete_flag = 'N') then
				   
			 update sta_trn_tfoliofreeze
			 set foliofreeze_remark = 'FolioFreeze Released',
				 delete_flag = 'Y',
				 update_date = now(),
				 update_by = in_action_by
			 where comp_gid = in_comp_gid 
			 and folio_gid = in_folio_gid
			 and foliofreeze_flag = 'Y' and delete_flag = 'N';
			 
			/* # update Folioremark against inward 
			 update sta_trn_tinward 
			 set inward_all_status = inward_all_status | v_inward_foliofreeze_released_status,
				 folio_remark = concat(folio_remark,' FolioFreeze Released,')
			 where folio_gid = in_folio_gid
			 and comp_gid = in_comp_gid 
			 and delete_flag = 'N';*/
		end if;
	-- end if;
END$$
DELIMITER ;
