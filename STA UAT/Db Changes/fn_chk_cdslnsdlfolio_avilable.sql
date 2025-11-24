DELIMITER $$
CREATE DEFINER=`root`@`%` FUNCTION `fn_chk_cdslnsdlfolio_avilable`(in_comp_gid int(10),in_depository_code varchar(32)) RETURNS text CHARSET latin1
BEGIN

declare v_result varchar(32) default 'Not Received';
declare v_folio_shares bigint default 0;
declare v_folio_no varchar(32);

if in_depository_code = 'NSDL' then
	set v_folio_no = '00999999';
elseif in_depository_code = 'CDSL' then
	set v_folio_no = '00888888';
end if;

select a.folio_shares into v_folio_shares
from sta_trn_tfolio as a
inner join sta_mst_tcompany as b on a.comp_gid = b.comp_gid
and b.delete_flag = 'N' and b.active_flag = 'Y'
where a.comp_gid = in_comp_gid
and a.folio_no = v_folio_no
and a.delete_flag = 'N';

set v_folio_shares = ifnull(v_folio_shares,0);

if v_folio_shares > 0 then
	set v_result = 'Not Received';
else 
	set v_result = 'Not Applicable';
end if;

RETURN v_result;
END$$
DELIMITER ;
