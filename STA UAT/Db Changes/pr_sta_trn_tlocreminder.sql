DELIMITER $$
drop procedure if exists pr_sta_trn_tlocreminder;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_trn_tlocreminder`(
  in in_locrem_gid int,
  in in_inward_gid int,
  in in_reminder_sent_date date,
  in in_reminder_mode char(1),
  in in_courier_gid int,
  in in_awb_no varchar(32),
  in in_remark varchar(255),
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare v_action varchar(16) default 'UPDATE';
  
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;

    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;

  set err_msg = concat('Error',char(13),char(10),'-----');
  
  if not exists(select receivedmode_gid from sta_mst_treceivedmode
      where receivedmode_code = in_reminder_mode
      and delete_flag = 'N') then
    set err_msg := concat(err_msg,char(13),char(10),'Invalid reminder mode,');
    set err_flag := true;
  end if;

  
  if not exists(select courier_gid from sta_mst_tcourier
      where courier_gid = in_courier_gid
      and delete_flag = 'N') then
    set err_msg := concat(err_msg,char(13),char(10),'Invalid courier,');
    set err_flag := true;
  end if;

  
  if in_awb_no = '' then
    set err_msg := concat(err_msg,char(13),char(10),'Awb no cannot be blank,');
    set err_flag := true;
  end if;

   if v_action = 'UPDATE' then
    
    if not exists(select locrem_gid from sta_trn_tlocreminder
        where locrem_gid = in_locrem_gid
        and delete_flag = 'N') then
      set err_msg := concat(err_msg,char(13),char(10),'Invalid record,');
      set err_flag := true;
    end if;

    if err_flag = false then
      START TRANSACTION;

      update sta_trn_tlocreminder set
        reminder_sent_date = now(),
        reminder_mode = in_reminder_mode,
        courier_gid = in_courier_gid,
        awb_no = in_awb_no,
        remark = in_remark,
        status = 'Sent',
        update_date = sysdate(),
        update_by = in_action_by
      where locrem_gid = in_locrem_gid
      and status = 'Yet to sent'
      and delete_flag = 'N';

      COMMIT;
    else
      set out_result = 0;
      set out_msg = err_msg;
      leave me;
    end if;

    set out_result = 1;
    set out_msg = 'Record updated successfully';
    leave me;
  end if;

END$$
DELIMITER ;
