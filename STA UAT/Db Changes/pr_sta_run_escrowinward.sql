DELIMITER $$
drop procedure if exists pr_sta_run_escrowinward;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_run_escrowinward`()
BEGIN
declare vrow_number INT DEFAULT 0; 
declare vCount int; 
-- declare vcomp_gid int;
-- declare vinward_gid int(11);

drop temporary table if exists temp_locreminder;

create temporary table temp_locreminder
select b.comp_gid,b.inward_gid
from sta_trn_tlocreminder as a
inner join sta_trn_tinward as b on a.inward_gid = b.inward_gid 
and b.reminder_flag = 'Y' 
and b.inward_status = 4 
and b.delete_flag = 'N' 
inner join sta_trn_tupload as c on b.upload_gid = c.upload_gid and c.delete_flag = 'N'
where datediff(now(),c.meeting_date) > 120 
and a.days = 120
and a.delete_flag = 'N';

-- Get the total number of rows
SELECT COUNT(*) INTO vCount FROM temp_locreminder;

WHILE vrow_number < vCount DO
  SET @sqlQuery = CONCAT(
      'SELECT comp_gid, inward_gid into @comp_gid,@inward_gid FROM temp_locreminder LIMIT ', vrow_number, ', 1'
  );

  PREPARE stmt3 FROM @sqlQuery;
  EXECUTE stmt3;
  DEALLOCATE PREPARE stmt3;

  call pr_sta_ins_tinward_tmescrow(@comp_gid,@inward_gid);
  
  SET vrow_number = vrow_number + 1;
END WHILE;
END$$
DELIMITER ;
