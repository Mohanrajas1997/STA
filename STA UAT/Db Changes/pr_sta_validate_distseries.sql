DELIMITER $$
-- drop procedure pr_sta_validate_distseries;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_validate_distseries`(in in_inward_gid int,
  in in_share_count int,
  in in_dist_from int,
  in in_dist_to int,
  out out_result int,
  out out_msg text
)
me:begin
  declare err_msg text default '';
  declare err_flag boolean default false;
  declare done int default 0;
  
  declare v_share_count int default 0;
  declare v_comp_gid int default 0;
  declare v_cert_inactive_status tinyint default 0;

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;
		set out_result = 0;
		set out_msg = 'SQLEXCEPTION';
  END;
  
  select comp_gid into v_comp_gid 
  from sta_trn_tinward 
  where inward_gid = in_inward_gid
  and delete_flag = 'N';
  
  set v_share_count = in_dist_to - in_dist_from + 1;

 if v_share_count <> in_share_count then
    set err_msg := concat(
        'Share count mismatch for distribution range (', in_dist_from, '-', in_dist_to, '): ',
        'Expected ', v_share_count,
        ' but received ', in_share_count
    );
    set err_flag := true;
 end if;
  
  select status_value into v_cert_inactive_status from sta_mst_tstatus
  where status_type = 'Certificate'
  and status_desc = 'Inactive'
  and delete_flag = 'N';

  set v_cert_inactive_status := ifnull(v_cert_inactive_status,0);
  
   if err_flag = false then
    if exists(select b.certdist_gid from sta_trn_tcert as a
      inner join sta_trn_tcertdist as b on b.cert_gid = a.cert_gid and b.delete_flag = 'N'
      where a.comp_gid = v_comp_gid
      and (in_dist_from between b.dist_from and b.dist_to
      or in_dist_to between b.dist_from and b.dist_to )
      and a.cert_status <> v_cert_inactive_status
      and a.delete_flag = 'N') then
      set err_msg := concat(err_msg,'Distinctive from(',in_dist_from,')',' /to(',in_dist_to,') intersects with available series');
      set err_flag := true;
    end if;
  end if;
   
  if err_flag = false then 
	set out_result = 1;
    set out_msg = 'Validation Success..!';
  else
	set out_result = 0;
    set out_msg = err_msg;
    delete from sta_trn_tcaentry 
    where inward_gid = in_inward_gid 
    and delete_flag = 'N';
  end if;
  
END$$
DELIMITER ;
