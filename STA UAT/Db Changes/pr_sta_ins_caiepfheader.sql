DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_ins_caiepfheader`(
in in_caiepfhdr_gid int(11),
in in_comp_gid int(11),
in in_inward_gid int(11),
in in_rta_internal_ref_no varchar(16),
in in_catype int(4),
in in_board_approval_date date,
in in_tot_credit_qty decimal(16,3),
in in_tot_credit_lockin_qty decimal(16,3),
in in_tot_debit_qty decimal(16,3),
in in_tot_debit_lockin_qty decimal(15,2),
in in_div_finyear varchar(16),
in in_tot_nominal_amtof_shares decimal(16,3),
in in_action_by varchar(16))
me:BEGIN
	#created by : Mohan
	#created date : 13/06/2025
  
  # insert in caiepfheader table
  if in_caiepfhdr_gid = 0 then
	  insert into sta_trn_tcaiepfheader 
				(comp_gid,inward_gid,caentry_slno,rta_internal_ref_no,catype,
				board_approval_date,tot_credit_qty,tot_credit_lockin_qty,tot_debit_qty,
				tot_debit_lockin_qty,div_finyear,tot_nominal_amtof_shares,insert_date,insert_by)
	  values 	(in_comp_gid,in_inward_gid,in_rta_internal_ref_no,in_caentry_slno,in_catype,
				in_board_approval_date,in_tot_credit_qty,in_tot_credit_lockin_qty,in_tot_debit_qty,
				in_tot_debit_lockin_qty,in_div_finyear,in_tot_nominal_amtof_shares,now(),in_action_by);
  else
  # update in caiepfheader table
	  update sta_trn_tcaiepfheader
      set caentry_slno = in_caentry_slno,
          rta_internal_ref_no = in_rta_internal_ref_no,
          catype = in_catype,
          board_approval_date = in_board_approval_date,
          tot_credit_qty = in_tot_credit_qty,
          tot_credit_lockin_qty = in_tot_credit_lockin_qty,
          tot_debit_qty = in_tot_debit_qty,
		  tot_debit_lockin_qty = in_tot_debit_lockin_qty,
          div_finyear = in_div_finyear,
          tot_nominal_amtof_shares = in_tot_nominal_amtof_shares,
          update_date = now(),
          update_by = in_action_by
	where inward_gid = in_inward_gid 
    and caiepfhdr_gid = in_caiepfhdr_gid
    and delete_flag = 'N';
  end if;								
END$$
DELIMITER ;
