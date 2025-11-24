DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_ins_tdividend_contactcomp`(
IN In_file_gid int,
IN In_comp_code varchar(16),
IN In_finyear_code varchar(32),
IN In_accno varchar(32),
IN In_folio_dpid varchar(32),
IN In_share_holder varchar(128),
IN In_share_count int(10),
IN In_div_rate double(15,2),
IN In_div_amount double(15,2),
IN In_tds_percent double(7,2),
IN In_tds_amount double(15,2),
IN In_net_amount double(15,2),
IN In_curr_amount double(15,2),
IN In_div_date date,
IN In_warrant_no varchar(128),
IN In_div_paymode varchar(128),
IN In_div_payrefno varchar(32),
IN In_joint1 varchar(128),
IN In_joint2 varchar(128),
IN In_holder1_pan varchar(128),
IN In_holder1_email varchar(128),
IN In_holder1_addr1 varchar(128),
IN In_holder1_addr2 varchar(128),
IN In_holder1_addr3 varchar(128),
IN In_holder1_city varchar(128),
IN In_holder1_state varchar(128),
IN In_holder1_country varchar(128),
IN In_holder1_pincode varchar(128),
IN In_holder1_bank_name varchar(128),
IN In_holder1_bank_branch varchar(128),
IN In_holder1_acc_no varchar(128),
IN In_holder1_acc_type varchar(128),
IN In_holder1_micr_code varchar(128),
IN In_holder1_ifsc_code varchar(128),
IN In_holder1_category varchar(128),
IN In_div_remark varchar(255),
IN In_loginuser varchar(64),
IN In_line_no int,
IN In_errline_flag boolean,
out out_msg text,
out out_result int
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag varchar(10) default false;
  declare v_comp_gid int default 0;
  declare v_finyear_gid int default 0;
  declare v_acc_gid int default 0;
  declare v_paymode_gid int default 0;
  declare v_paymode_code char(1);
  
  DECLARE EXIT HANDLER FOR SQLEXCEPTION

  BEGIN
    ROLLBACK;
    set out_msg = 'SQLEXCEPTION';
    set out_result = 0;
    
    if in_errline_flag = true then
      call pr_sta_ins_errline(in_file_gid,in_line_no,out_msg);
    end if;

  END;
  
    select comp_gid into v_comp_gid from sta_mst_tcompany
    where comp_code = In_comp_code and delete_flag = 'N';
		set v_comp_gid = ifnull(v_comp_gid,0);
    if v_comp_gid = 0 then
		set err_msg := concat(err_msg,'Invalid company code,');
		set err_flag := true;
	end if;
   
   select finyear_gid into v_finyear_gid from sta_mst_tfinyear
   where finyear_code = in_finyear_code and delete_flag = 'N';
		set v_finyear_gid = ifnull(v_finyear_gid,0);    
	if v_finyear_gid = 0 then
		set err_msg := concat(err_msg,'Invalid Financial Year,');
		set err_flag := true;
	end if;
   
	select acc_gid into v_acc_gid from div_mst_tacc
	where acc_no = In_accno and delete_flag = 'N';
		set v_acc_gid = ifnull(v_acc_gid,0);    
	if v_acc_gid = 0 then
		set err_msg := concat(err_msg,'Invalid Account No,');
		set err_flag := true;
	end if;
   
   if In_accno='' then
        set err_msg := concat(err_msg,'Account No is empty,');
        set err_flag :=true;
    end if;
   
	if In_share_holder='' then
        set err_msg := concat(err_msg,'Share Holder is empty,');
        set err_flag :=true;
    end if;
    
	if In_share_count='' or In_share_count < 0 then
        set err_msg := concat(err_msg,'Invalid Share Count,');
        set err_flag :=true;
    end if;
    
	if In_div_amount='' or In_div_amount < 0 then
        set err_msg := concat(err_msg,'Invalid Dividend Amount,');
        set err_flag :=true;
    end if;
    
	if In_div_date=''  then
        set err_msg := concat(err_msg,'Dividend Date is empty,');
        set err_flag :=true;
    end if;    
    
    select paymode_gid,paymode_code into v_paymode_gid,v_paymode_code from sta_mst_tpaymode
    where paymode_desc = In_div_paymode and delete_flag = 'N';
		set v_paymode_gid = ifnull(v_paymode_gid,0);
    if v_paymode_gid = 0 then
		set err_msg := concat(err_msg,'Invalid Paymode code,');
		set err_flag := true;
	end if;
    
   if err_flag = false then
    if exists(select div_gid from div_trn_tdividend
      where comp_gid = v_comp_gid
      and finyear_gid = v_finyear_gid and folio_dpid = In_folio_dpid
      and delete_flag = 'N') then
      set err_msg := concat(err_msg,'Duplicate Folio Dpid,');
      set err_flag := true;
    end if;
    
end if;

if err_flag = true then
    set out_result = 0;
    set out_msg = err_msg;
    if in_errline_flag = true then
      call pr_sta_ins_errline(in_file_gid,in_line_no,out_msg);
    end if;
    leave me;
end if;
  
if err_flag = false then
	start transaction;
       insert into div_trn_tdividend(file_gid, comp_gid, finyear_gid, acc_gid, folio_dpid,
       shar_holder,share_count,div_rate,div_amount,tds_per,tds_amount,net_amount,warrant_no,
       curr_amount,div_status,div_date,div_pay_mode,div_ref_no,issue_date,issue_pay_mode,issue_ref_no,
       joint1_name,joint2_name,holder1_pan,holder1_email,holder1_addr1,holder1_addr2,holder1_addr3,
       holder1_city,holder1_state,holder1_country,holder1_pincode,holder1_bank_name,
       holder1_bank_branch,holder1_acc_no,holder1_acc_type,holder1_micr_code,
       holder1_ifsc_code,holder1_category,div_remark,insert_by, insert_date)
       values(In_file_gid,v_comp_gid,v_finyear_gid,v_acc_gid,In_folio_dpid,In_share_holder,
       In_share_count,In_div_rate,In_div_amount,In_tds_percent,In_tds_amount,In_net_amount,In_warrant_no,
       In_curr_amount,'C',In_div_date,v_paymode_code,In_div_payrefno,In_div_date,v_paymode_code,In_div_payrefno,
       In_joint1,In_joint2,In_holder1_pan,In_holder1_email,In_holder1_addr1,In_holder1_addr2,In_holder1_addr3,
       In_holder1_city,In_holder1_state,In_holder1_country,In_holder1_pincode,In_holder1_bank_name,
       In_holder1_bank_branch,In_holder1_acc_no,In_holder1_acc_type,In_holder1_micr_code,
       In_holder1_ifsc_code,In_holder1_category,In_div_remark,In_loginuser,now());       
	COMMIT;
 set out_msg = 'Record Inserted Successfully !';

ELSE
  set out_result = 0;
  set out_msg = err_msg ;
  leave me;
END IF;
  set out_result = 1;  
END$$
DELIMITER ;
