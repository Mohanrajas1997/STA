DELIMITER $$
CREATE DEFINER=`root`@`%` FUNCTION `fn_sta_get_escrowfoliono`(in_comp_gid int
) RETURNS varchar(16)
begin
  declare v_escrowfolio_no varchar(16);

  select
    folio_no
  into
    v_escrowfolio_no
  from sta_trn_tfolio
  where comp_gid = in_comp_gid
  and folio_sno = '66666'
  and delete_flag = 'N';

  set v_escrowfolio_no = ifnull(v_escrowfolio_no,'66666');

  return v_escrowfolio_no;
END$$
DELIMITER ;
