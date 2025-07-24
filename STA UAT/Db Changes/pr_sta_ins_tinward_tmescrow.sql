DELIMITER $$
drop procedure if exists pr_sta_ins_tinward_tmescrow;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_ins_tinward_tmescrow`(in in_comp_gid int(11),
in in_inward_gid int(11))
me:BEGIN
	declare v_comp_short_code varchar(16);
    declare v_inward_gid int(11) default 0;
	declare v_inward_sno varchar(16);
    declare v_inward_comp_no varchar(16);
    declare v_inward_comp_sno varchar(16);
    declare v_inward_no varchar(16);
	declare v_awb_no varchar(32) default '0987654321';
    declare v_entity_gid int default 1;
    declare v_inward_received_status int default 0;
    declare v_queue_maker_status int default 0;
    declare v_folio_no varchar(32) ;
    declare v_holder1_name varchar(32);
    declare v_shareholder_pan_no varchar(16);
    declare v_folio_gid int(11) default 0;
    declare v_tran_folio_gid int(11) default 0;
    declare v_action_by varchar(32) default 'System';
        
	select inward_sno,comp_short_code into  v_inward_sno,v_comp_short_code from sta_mst_tcompany where comp_gid=in_comp_gid and delete_flag='N';
    set v_inward_comp_sno=(select LPAD(v_inward_sno,'4','0'));
    set v_inward_comp_no=concat(v_comp_short_code,v_inward_comp_sno);
    
    select max(inward_no) into v_inward_no from sta_trn_tinward
	where entity_gid = v_entity_gid
	and delete_flag = 'N';

	set v_inward_no = ifnull(v_inward_no,0);
	set v_inward_no = v_inward_no + 1;
    
    select folio_gid,folio_no,holder1_name,holder1_pan_no
    into v_tran_folio_gid,v_folio_no,v_holder1_name,v_shareholder_pan_no
    from sta_trn_tfolio where folio_sno = '66666'
    and comp_gid = in_comp_gid
    and delete_flag = 'N';
    
    select 
    case when tran_folio_gid > 0 then tran_folio_gid else folio_gid end 
    into v_folio_gid 
    from sta_trn_tinward where inward_gid = in_inward_gid
    and reminder_flag = 'Y'
    and delete_flag = 'N';
    
    select status_value into v_inward_received_status from sta_mst_tstatus
	where status_type = 'Inward'
	and status_desc = 'Received'
	and delete_flag = 'N';

	set v_inward_received_status = ifnull(v_inward_received_status,0);
    
	select status_value into v_queue_maker_status from sta_mst_tstatus
    where status_type = 'Queue'
    and status_desc = 'Maker'
    and delete_flag = 'N';

    set v_queue_maker_status = ifnull(v_queue_maker_status,0);
    
  START TRANSACTION;
  if not exists (select '*' from sta_trn_tinward where folio_gid = v_folio_gid
				  and tran_folio_gid = v_tran_folio_gid 
				  and tran_code = 'TM'
				  and delete_flag = 'N') then
      INSERT INTO sta_trn_tinward
      (
        entity_gid,comp_gid,inward_no,inward_comp_no,inward_comp_sno,received_date,received_mode,courier_gid,awb_no,tran_code,docsubtype_code,
        shareholder_name,inward_status,inward_all_status,queue_status,queue_all_status,
        folio_gid,tran_folio_gid,folio_no,shareholder_addr,shareholder_pan_no,shareholder_contact_no,shareholder_email_id,
        inward_remark,insert_date,insert_by,share_count
      ) values
      (
        v_entity_gid,in_comp_gid,v_inward_no,v_inward_comp_no,v_inward_sno,now(),'C',4,v_awb_no,'TM','',
        v_holder1_name,v_inward_received_status,v_inward_received_status,v_queue_maker_status,v_queue_maker_status,
        v_folio_gid,v_tran_folio_gid,v_folio_no,'',v_shareholder_pan_no,'','',
        'After 120 Days Escrow Account entry',sysdate(),v_action_by,0
      );

	  UPDATE sta_mst_tcompany SET inward_sno=inward_sno+1 WHERE comp_gid=in_comp_gid and delete_flag='N';
      COMMIT;

      select max(inward_gid) into v_inward_gid from sta_trn_tinward;
      select v_inward_gid,v_action_by;
      call pr_sta_ins_queue(v_inward_gid,v_action_by,'N','M','',@queue_gid,@out_result,@out_msg);
  end if;
END$$
DELIMITER ;
