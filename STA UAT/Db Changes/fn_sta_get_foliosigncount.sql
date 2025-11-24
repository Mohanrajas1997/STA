DELIMITER $$
drop function if exists fn_sta_get_foliosigncount;
CREATE DEFINER=`root`@`%` FUNCTION `fn_sta_get_foliosigncount`(
  in_comp_gid int,
  in_count_type varchar(32)
) RETURNS int(10)
begin
  declare v_count int(10) default 0; 
  
  if in_count_type = 'Available' then 
	  select
		count(b.signature_gid)
	  into
		v_count
	  from sta_mst_tcompany as a
	  inner join sta_trn_tfolio as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'
	  where b.signature_gid > 0 and b.folio_shares > 0
      and b.folio_no not in('00555555','00666666','00777777','00888888','00999999')
      and a.comp_gid = in_comp_gid
	  and a.active_flag = 'Y'
	  and a.delete_flag = 'N';
  else
	select
		count(signature_gid)
	  into
		v_count
	  from sta_mst_tcompany as a
	  inner join sta_trn_tfolio as b on a.comp_gid = b.comp_gid and b.delete_flag = 'N'
	  where b.signature_gid = 0 and b.folio_shares > 0
      and b.folio_no not in('00555555','00666666','00777777','00888888','00999999')
      and a.comp_gid = in_comp_gid
	  and a.active_flag = 'Y'
	  and a.delete_flag = 'N';
  end if;

  set v_count = ifnull(v_count,0);

  return v_count;
END$$
DELIMITER ;
