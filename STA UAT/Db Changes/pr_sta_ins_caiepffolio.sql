DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_ins_caiepffolio`(
in in_caiepffolio_gid int(11),
in in_inward_gid int(11),
in in_caiepffolio_slno decimal(10,0),
in in_dp_id varchar(32),
in in_client_id varchar(32),
in in_folio_no varchar(32),
in in_credit_qty decimal(16,3),
in in_debit_qty decimal(16,3),
in in_holder1_name varchar(255),
in in_investor_category varchar(64),
in in_creditqty_lockin_reasoncode int(2),
in in_creditqty_lockin_releasedate date,
in in_debitqty_lockin_reasoncode int(2),
in in_debitqty_lockin_releasedate date,
in in_bo_address1 varchar(64),
in in_bo_address2 varchar(64),
in in_bo_address3 varchar(64),
in in_bo_address_city varchar(64),
in in_bo_address_state varchar(64),
in in_bo_address_country varchar(64),
in in_bo_address_pincode varchar(4),
in in_action_by varchar(16))
me:BEGIN
	#created by : Mohan
	#created date : 13/06/2025
  
  # insert in caiepffolio table
  insert into sta_trn_tcaiepffolio
			(inward_gid,caiepffolio_slno,dp_id,client_id,folio_no,credit_qty,
			debit_qty,holder1_name,investor_category,creditqty_lockin_reasoncode,
			creditqty_lockin_releasedate,debitqty_lockin_reasoncode,debitqty_lockin_releasedate,
			bo_address1,bo_address2,bo_address3,bo_address_city,bo_address_state,
			bo_address_country,bo_address_pincode,insert_date,insert_by)
  values 	(in_inward_gid,in_caiepffolio_slno,in_dp_id,in_client_id,in_folio_no,in_credit_qty,
			in_debit_qty,in_holder1_name,in_investor_category,in_creditqty_lockin_reasoncode,
			in_creditqty_lockin_releasedate,in_debitqty_lockin_reasoncode,in_debitqty_lockin_releasedate,
			in_bo_address1,in_bo_address2,in_bo_address3,in_bo_address_city,in_bo_address_state,
			in_bo_address_country,in_bo_address_pincode,now(),in_action_by);			
END$$
DELIMITER ;
