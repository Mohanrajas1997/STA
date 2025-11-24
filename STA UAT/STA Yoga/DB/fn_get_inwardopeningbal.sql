DELIMITER $$
CREATE DEFINER=`root`@`%` FUNCTION `fn_get_inwardopeningbal`(in_tran_code varchar(16),in_docsubtype_code varchar(16),
in_period_from date) RETURNS int(11)
BEGIN
declare v_opening_count int(11) default 0;

if (in_tran_code = 'DM' || in_tran_code = 'LS' || in_tran_code = 'IS') then
	select 
		count(a.tran_code) into v_opening_count
	from sta.sta_trn_tinward as a	
	inner join sta.sta_mst_ttrantype as b on a.tran_code = b.trantype_code
	and b.delete_flag = 'N'
	where a.tran_code in (in_tran_code)
    and a.received_date < in_period_from
	and (a.inward_status = 1 or a.inward_status = 2)
	group by a.tran_code;
    
elseif ((in_tran_code = 'TM') and (in_docsubtype_code = 'TM' or in_docsubtype_code = 'ND'
		or in_docsubtype_code = 'NC' or in_docsubtype_code = 'IT')) then
	select 
		count(b.docsubtype_code) into v_opening_count
	from sta.sta_trn_tinward as a	
	inner join sta.sta_mst_tdocsubtype as b on a.tran_code = b.trantype_code
	and a.docsubtype_code = b.docsubtype_code and b.delete_flag = 'N'
	where a.tran_code = in_tran_code and a.docsubtype_code = in_docsubtype_code
	and a.received_date < in_period_from
	and (a.inward_status = 1 or a.inward_status = 2);

elseif ((in_tran_code = 'OT') and (in_docsubtype_code = 'DM' or in_docsubtype_code = 'LS'
		or in_docsubtype_code = 'IE' or in_docsubtype_code = 'OT')) then
	select 
		count(b.docsubtype_code) into v_opening_count
	from sta.sta_trn_tinward as a	
	inner join sta.sta_mst_tdocsubtype as b on a.tran_code = b.trantype_code
	and a.docsubtype_code = b.docsubtype_code and b.delete_flag = 'N'
	where a.tran_code = in_tran_code and a.docsubtype_code = in_docsubtype_code
	and a.received_date < in_period_from
	and (a.inward_status = 1 or a.inward_status = 2) ;
end if; 

set v_opening_count = ifnull(v_opening_count,0);

RETURN v_opening_count;
END$$
DELIMITER ;
