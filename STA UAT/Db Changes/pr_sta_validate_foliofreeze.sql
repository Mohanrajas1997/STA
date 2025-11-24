DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_validate_foliofreeze`(
  in in_inward_gid int,
  in in_comp_gid int,
  in in_folio_gid int,
  in in_trantype_code varchar(64),
  in in_transubtype_code varchar(64),
  out out_result int,
  out out_msg text
)
me:begin
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare done int default 0;
  
  declare v_comp_listed char(1) default 'N';
  
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
 
 #Get Company listed Flag Y/N
 select comp_listed into v_comp_listed
 from sta_mst_tcompany 
 where comp_gid = in_comp_gid and delete_flag = 'N';
 
 set v_comp_listed = ifnull(v_comp_listed,'N');
 
  #Check folio has kyc availabe (or) not 
  if (v_comp_listed = 'Y' and EXISTS (SELECT 1 FROM sta_mst_ttrantype 
									  WHERE trantype_code = in_trantype_code
									  AND foliofreeze_flag = 'Y' AND delete_flag = 'N')
	  ) then
    if exists (select 1 from sta_trn_tfoliofreeze 
			   where comp_gid = in_comp_gid 
			   and folio_gid = in_folio_gid
			   and foliofreeze_flag = 'Y' and delete_flag = 'N') then 
		set err_msg := concat(err_msg,'Folio Freezed');
		set err_flag := true;
	end if;
  end if;
   
  if err_flag = false then 
	set out_result = 1;
    set out_msg = 'Valid Folio';
  else
	set out_result = 0;
    set out_msg = err_msg;
  end if;
  
END$$
DELIMITER ;
