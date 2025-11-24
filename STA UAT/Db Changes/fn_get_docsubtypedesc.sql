DELIMITER $$
CREATE DEFINER=`root`@`%` FUNCTION `fn_get_docsubtypedesc`(in_inward_gid int(10),in_trantype_code varchar(32)) RETURNS text CHARSET latin1
BEGIN
declare v_result text;

select group_concat(docsubtype_desc) into v_result
from sta_trn_tinward as a
inner join sta_trn_tinwarddocsubtype as b on a.inward_gid = b.inward_gid
and b.delete_flag = 'N'
where a.inward_gid = in_inward_gid and b.trantype_code = in_trantype_code 
and a.delete_flag = 'N'; 

set v_result = ifnull(v_result,''); 

if v_result = '' then 
	select b.docsubtype_desc into v_result from sta_trn_tinward as a
    inner join sta_mst_tdocsubtype as b on a.tran_code = b.trantype_code
    and a.docsubtype_code = b.docsubtype_code and b.delete_flag = 'N'
    where a.inward_gid = in_inward_gid and a.tran_code = in_trantype_code 
	and a.delete_flag = 'N'; 
end if;

set v_result = ifnull(v_result,'');

RETURN v_result;
END$$
DELIMITER ;
