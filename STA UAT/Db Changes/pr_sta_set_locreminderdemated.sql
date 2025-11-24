DELIMITER $$
drop procedure if exists pr_sta_set_locreminderdemated;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_set_locreminderdemated`(
  in in_inward_gid int,
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare v_old_folio_gid int default 0;
  declare v_new_folio_gid int default 0;
  declare v_tran_code char(2) default null;
  declare v_inward_gid int default 0;
  
  declare err_msg text default '';
  declare err_flag boolean default false;

  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;

    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;

    select
     a.folio_gid
    into
      v_old_folio_gid
    from sta_trn_tinward as a
    inner join sta_mst_ttrantype as b on b.trantype_code = a.tran_code and b.delete_flag = 'N'
    left join sta_trn_tupload as c on c.upload_gid = a.upload_gid and c.delete_flag = 'N'
    where a.inward_gid = in_inward_gid
    and a.tran_code = 'DM'
    and a.delete_flag = 'N';
    
    select v_old_folio_gid;    
    
    if v_old_folio_gid > 0 then
        -- From Folio cases ()
		select 
			inward_gid,tran_code
		into
			v_inward_gid,v_tran_code 
        from sta_trn_tinward 
		where folio_gid = v_old_folio_gid
        and chklst_valid > 0
        -- and update_completed = 'Y'
        -- and inward_completed = 'Y'
        and inward_status = 4
        and reminder_flag = 'Y'
        and delete_flag = 'N';
        select v_inward_gid,v_tran_code;
        set v_tran_code = ifnull(v_tran_code,'');
        
        if v_tran_code <> '' then 
			update sta_trn_tinward 
            set reminder_flag = 'N',
				reminder_status = 'Demated'
            where inward_gid = v_inward_gid
            and chklst_valid > 0
			and update_completed = 'Y'
			and inward_completed = 'Y'
			and inward_status = 4
            and reminder_flag = 'Y'
			and delete_flag = 'N';
            
            update sta_trn_tlocreminder 
            set status = 'Demated'
            where inward_gid = v_inward_gid
            and status = 'Yet to Sent'
            and delete_flag = 'N';
        end if;
        
        -- To Folio case
        select 
			inward_gid,tran_code
		into 
			v_inward_gid,v_tran_code
        from sta_trn_tinward 
		where tran_folio_gid = v_old_folio_gid
        and chklst_valid > 0
        and update_completed = 'Y'
        and inward_completed = 'Y'
        and inward_status = 4
        and reminder_flag = 'Y'
        and delete_flag = 'N';
        select v_inward_gid,v_tran_code;
        set v_tran_code = ifnull(v_tran_code,'');
        
        if v_tran_code <> '' then 
			update sta_trn_tinward 
            set reminder_flag = 'N',
				reminder_status = 'Demated'
            where inward_gid = v_inward_gid
            and chklst_valid > 0
			and update_completed = 'Y'
			and inward_completed = 'Y'
			and inward_status = 4
            and reminder_flag = 'Y'
			and delete_flag = 'N';
            
            update sta_trn_tlocreminder set status = 'Demated'
            where inward_gid = v_inward_gid
            and status = 'Yet to Sent'
            and delete_flag = 'N';
        end if;
    end if;
    
  set out_result = 1;
  set out_msg = 'Record updated successfully';
END$$
DELIMITER ;
