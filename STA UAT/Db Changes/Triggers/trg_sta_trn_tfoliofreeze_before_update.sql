DELIMITER $$

CREATE TRIGGER `trg_sta_trn_tfoliofreeze_before_update`
BEFORE UPDATE ON `sta_trn_tfoliofreeze`
FOR EACH ROW
BEGIN
    INSERT INTO `sta_trn_tfoliofreeze_audit` (
        `foliofreeze_gid`,
        `comp_gid`,
        `folio_gid`,
        `foliofreeze_date`,
        `foliofreeze_remark`,
        `foliofreeze_flag`,
        `insert_date`,
        `insert_by`,
        `update_date`,
        `update_by`,
        `delete_flag`
    ) VALUES (
        OLD.`foliofreeze_gid`,
        OLD.`comp_gid`,
        OLD.`folio_gid`,
        OLD.`foliofreeze_date`,
        OLD.`foliofreeze_remark`,
        OLD.`foliofreeze_flag`,
        OLD.`insert_date`,
        OLD.`insert_by`,
        OLD.`update_date`,
        OLD.`update_by`,
        OLD.`delete_flag`
    );
END$$

DELIMITER ;
