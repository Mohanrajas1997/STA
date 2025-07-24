drop procedure if exists pr_sta_get_caiepfheader;
DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_get_caiepfentry`(
  in_inward_gid int,
  in_queue_gid int)
BEGIN
if in_queue_gid = 0 then
    set in_queue_gid = null;
  end if;

  select
	c.comp_gid,
    c.comp_name,
    i.inward_comp_no as inward_no,
    i.inward_gid,
    h.rta_internal_ref_no,
    h.catype,
	h.board_approval_date,
    -- date_format(ifnull(h.board_approval_date,0000-00-00),'%Y-%m-%d') as board_approval_date,
	h.execution_date,
	-- date_format(ifnull(h.execution_date,0000-00-00),'%Y-%m-%d') as execution_date,
	h.tot_credit_qty,
	h.tot_credit_lockin_qty,
	h.tot_debit_qty,
	h.tot_debit_lockin_qty,
	h.div_finyear,
	h.tot_nominal_amtof_shares,
    i.tran_code,
    i.chklst_valid,
    i.chklst_disc,
    i.queue_gid,
    q.action_status
  from sta_trn_tinward as i
  inner join sta_mst_tcompany as c on c.comp_gid = i.comp_gid and c.delete_flag = 'N'
  inner join sta_trn_tqueue as q on q.queue_gid = i.queue_gid and q.delete_flag = 'N'
  left join sta_trn_tcaiepfheader as h on h.inward_gid = i.inward_gid and h.delete_flag = 'N'
  where i.inward_gid = in_inward_gid
  and i.queue_gid = ifnull(in_queue_gid,i.queue_gid)
  and i.delete_flag = 'N';
  
  select
    a.caiepffolio_gid,
    a.inward_gid,
    a.caiepffolio_slno,
	a.dp_id,
	a.client_id,
	a.folio_no,
	a.credit_qty,
	a.debit_qty,
	a.holder1_name,
	a.investor_category,
	a.creditqty_lockin_reasoncode,
	a.creditqty_lockin_releasedate,
    -- null as creditqty_lockin_releasedate,
    -- date_format(a.creditqty_lockin_releasedate,'%Y-%m-%d') as creditqty_lockin_releasedate,
	a.debitqty_lockin_reasoncode,
	a.debitqty_lockin_releasedate,
    -- null as debitqty_lockin_releasedate,
    -- date_format(a.debitqty_lockin_releasedate,'%Y-%m-%d') as debitqty_lockin_releasedate,
	a.bo_address1,
	a.bo_address2,
	a.bo_address3,
	a.bo_address_city,
	a.bo_address_state,
	a.bo_address_country,
	a.bo_address_pincode
  from sta_trn_tinward as i
  inner join sta_trn_tcaiepffolio as a on a.inward_gid = i.inward_gid and a.delete_flag = 'N'
  where i.inward_gid = in_inward_gid
  and i.delete_flag = 'N';

END$$
DELIMITER ;
