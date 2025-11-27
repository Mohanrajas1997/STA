DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_ins_caiepffolio`(
in in_caiepffolio_gid int(11),
in in_inward_gid int(11),
in in_caiepffolio_slno decimal(10,0),
in in_dp_id varchar(32),
in in_client_id varchar(32),
in in_folio_no varchar(32),
in in_dist_from int(11),
in in_dist_to int(11),
in in_credit_qty int(11),
in in_debit_qty int(11),
in in_holder1_name varchar(255),
in in_holder2_name varchar(255),
in in_holder3_name varchar(255),
in in_holder4_name varchar(255),
in in_holder1_fhname varchar(255),
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
    #updated by : Mohan
	#updated date : 27/11/2025
   
  # insert in caiepffolio table
  insert into sta_trn_tcaiepffolio
			(inward_gid,caiepffolio_slno,dp_id,client_id,folio_no,dist_from,dist_to,
            credit_qty,debit_qty,holder1_name,holder2_name,holder3_name,holder4_name,
            holder1_fhname,investor_category,creditqty_lockin_reasoncode,
			creditqty_lockin_releasedate,debitqty_lockin_reasoncode,debitqty_lockin_releasedate,
			bo_address1,bo_address2,bo_address3,bo_address_city,bo_address_state,
			bo_address_country,bo_address_pincode,insert_date,insert_by)
  values 	(in_inward_gid,in_caiepffolio_slno,in_dp_id,in_client_id,in_folio_no,in_dist_from,in_dist_to,
			in_credit_qty,in_debit_qty,in_holder1_name,in_holder2_name,in_holder3_name,in_holder4_name,
            in_holder1_fhname,in_investor_category,in_creditqty_lockin_reasoncode,
			in_creditqty_lockin_releasedate,in_debitqty_lockin_reasoncode,in_debitqty_lockin_releasedate,
			in_bo_address1,in_bo_address2,in_bo_address3,in_bo_address_city,in_bo_address_state,
			in_bo_address_country,in_bo_address_pincode,now(),in_action_by);

END$$
DELIMITER ;
