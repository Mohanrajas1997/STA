DELIMITER $$
drop event locescrowinward_event;
CREATE DEFINER=`root`@`%` EVENT `locescrowinward_event`
ON SCHEDULE EVERY 1 DAY
STARTS '2025-05-27 05:00:00'
ON COMPLETION NOT PRESERVE
ENABLE
DO
BEGIN
     CALL pr_sta_run_escrowinward();
END$$

DELIMITER ;
