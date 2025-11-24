DELIMITER $$
CREATE DEFINER=`root`@`%` FUNCTION `fn_get_sharequantity`(in_isin_id varchar(64)
) RETURNS int(11)
begin
  declare v_share_qty int default 0;

  select
    share_qty
  into
    v_share_qty
  from sta_mst_tcompany
  where isin_id = in_isin_id
  and delete_flag = 'N';
  
  set v_share_qty = ifnull(v_share_qty,0);

  return v_share_qty;
END$$
DELIMITER ;
