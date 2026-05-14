DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_certentrydcrf`(
  in in_inward_gid int,
  in in_cert_gid text,
  in in_src_folio_gid int,
  in in_chklst_valid int,
  in in_chklst_disc int,
  in in_remark varchar(255),
  in in_action_status tinyint,
  in in_action_by varchar(16),
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare v_action_success_status tinyint default 0;
  declare v_action_reject_status tinyint default 0;
  declare v_queue_gid int default 0;
  declare v_queue_action_status tinyint default 0;
  declare v_queue_to char(1);
  declare v_inward_inprocess_status tinyint default 0;
  declare v_inward_reject_status tinyint default 0;
  declare v_inward_status tinyint default 0;
  declare v_cert_gid int default 0;
  declare v_folio_gid int default 0;
  declare v_received_date date default null;

  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;

    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;

  
  if in_inward_gid = 0 then
    set err_msg := concat(err_msg,'Inward gid is zero,');
    set err_flag := true;
  end if;

  
  if in_cert_gid = "" then
    set err_msg := concat(err_msg,'Certificate gid is blank,');
    set err_flag := true;
  end if;

  if in_chklst_disc = 0 then
    
    select fn_sta_chk_foliosign(in_src_folio_gid) into @result;

    if @result = 0 then
      set err_msg := concat(err_msg,'Seller folio signature is not available,');
      set err_flag := true;
    end if;
  end if;

  select
    queue_gid,dematpend_gid,received_date
  into
    v_queue_gid,v_received_date
  from sta_trn_tinward
  where inward_gid = in_inward_gid
  and delete_flag = 'N';

  set v_queue_gid := ifnull(v_queue_gid,0);
  set v_received_date := ifnull(v_received_date,curdate());

  if v_queue_gid = 0 then
    set err_msg := concat(err_msg,'Queue gid not found,');
    set err_flag := true;
  end if;

  select action_status into v_queue_action_status from sta_trn_tqueue
  where queue_gid = v_queue_gid
  and delete_flag = 'N';

  set v_queue_action_status := ifnull(v_queue_action_status,0);

  if v_queue_action_status > 0 then
    set err_msg := concat(err_msg,'Queue already updated,');
    set err_flag := true;
  end if;

  
  if err_flag = false then
    
    update sta_trn_tinward set
      tran_folio_gid = v_folio_gid,
      chklst_valid = in_chklst_valid,
      chklst_disc = in_chklst_disc
    where inward_gid = in_inward_gid
    and delete_flag = 'N';

    call pr_sta_set_queuemove(in_inward_gid,in_remark,in_action_status,in_action_by,@result,@msg);

    if @result = 0 then
      set err_msg := concat(err_msg,@msg);
      set err_flag := true;
    else
      set out_msg = @msg;
    end if;
  end if;

  if err_flag = false then
    START TRANSACTION;

    
    update sta_trn_tcertentry set delete_flag = 'Y' where inward_gid = in_inward_gid and delete_flag = 'N';

    
    set @cert_gid = in_cert_gid;

    while (locate(',',@cert_gid) > 0)
    do
      set @value = cast(substr(@cert_gid,1,locate(',',@cert_gid)-1) as unsigned);

      insert into sta_trn_tcertentry
      (
        inward_gid,
        cert_gid
      )
      values
      (
        in_inward_gid,
        @value
      );

      set @cert_gid = substr(@cert_gid,locate(',',@cert_gid)+1);
    end while;

    COMMIT;
  else
    set out_result = 0;
    set out_msg = err_msg;
    leave me;
  end if;

  set out_result = 1;
END$$
DELIMITER ;
