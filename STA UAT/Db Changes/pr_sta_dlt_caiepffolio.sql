DELIMITER $$
CREATE PROCEDURE `pr_sta_dlt_caiepffolio`(in in_inward_gid int(11))
BEGIN
delete from sta_trn_tcaiepffolio where inward_gid = in_inward_gid
and delete_flag = 'N';
END$$
DELIMITER ;
