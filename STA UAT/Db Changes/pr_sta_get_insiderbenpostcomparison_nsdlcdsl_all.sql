DELIMITER $$
drop procedure if exists pr_sta_get_insiderbenpostcomparison_nsdlcdsl_all;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_get_insiderbenpostcomparison_nsdlcdsl_all`(
  in_comp_gid int,
  in_benpost_date date
)
me:begin
  declare v_nsdl_benpost_from date;
  declare v_nsdl_benpost_to date;

  declare v_cdsl_benpost_from date;
  declare v_cdsl_benpost_to date;
  
  set v_nsdl_benpost_from = in_benpost_date;
  set v_nsdl_benpost_to = in_benpost_date;
  
  set v_cdsl_benpost_from = in_benpost_date;
  set v_cdsl_benpost_to = in_benpost_date;

  /*select max(benpost_date)
  into 	 v_nsdl_benpost_to
  from 	 sta_trn_tbenpost
  where  comp_gid = in_comp_gid
  and 	 benpost_date <= in_benpost_date
  and 	 depository_code = 'N'
  and 	 delete_flag = 'N';

  if v_nsdl_benpost_to is null then
    select min(benpost_date)
    into   v_nsdl_benpost_to
    from   sta_trn_tbenpost
    where  comp_gid = in_comp_gid
    and    benpost_date > in_benpost_date
    and    depository_code = 'N'
    and    delete_flag = 'N';
  end if;

  set v_nsdl_benpost_to = ifnull(v_nsdl_benpost_to,in_benpost_date);

  select max(benpost_date)
  into   v_nsdl_benpost_from
  from   sta_trn_tbenpost
  where  comp_gid = in_comp_gid
  and    benpost_date < v_nsdl_benpost_to
  and    depository_code = 'N'
  and    delete_flag = 'N';

  set v_nsdl_benpost_from = ifnull(v_nsdl_benpost_from,v_nsdl_benpost_to);

  select max(benpost_date)
  into   v_cdsl_benpost_to
  from   sta_trn_tbenpost
  where  comp_gid = in_comp_gid
  and    benpost_date <= in_benpost_date
  and    depository_code = 'C'
  and    delete_flag = 'N';

  if v_cdsl_benpost_to is null then
    select min(benpost_date)
    into   v_cdsl_benpost_to
    from   sta_trn_tbenpost
    where  comp_gid = in_comp_gid
    and    benpost_date > in_benpost_date
    and    depository_code = 'C'
    and    delete_flag = 'N';
  end if;

  set v_cdsl_benpost_to = ifnull(v_cdsl_benpost_to,in_benpost_date);

  select max(benpost_date)
  into   v_cdsl_benpost_from
  from   sta_trn_tbenpost
  where  comp_gid = in_comp_gid
  and    benpost_date < v_cdsl_benpost_to
  and    depository_code = 'C'
  and    delete_flag = 'N';

  set v_cdsl_benpost_from = ifnull(v_cdsl_benpost_from,v_nsdl_benpost_to);*/
  
  -- select v_nsdl_benpost_from,v_nsdl_benpost_to,v_cdsl_benpost_from,v_cdsl_benpost_to;

  select
      -- concat(c.dp_id,c.client_id) as 'Folio No',
      c.insider_name as 'Insider Name',
      c.pan_no as 'Pan No',
      c.depository_code as 'Depository Type',
      c.day1_closing as 'Open Bal',
      if(c.day1_closing > c.day2_closing,c.day1_closing - c.day2_closing,null) as selling,
      if(c.day1_closing < c.day2_closing,c.day2_closing - c.day1_closing,null) as buying,
      c.day2_closing as 'Close Bal'
      -- c.holder1_name as 'Shareholder Name',
	  -- c.holder1_addr1 as 'Address1',
	  -- c.holder1_addr2 as 'Address2',
      -- c.holder1_addr3 as 'Address3',
      -- c.holder1_pin as 'Pin',
      -- if(c.day1_closing = c.day2_closing,0,null) as nil
  from
  (
	  select
		a.isin_id,
		a.dp_id,
		a.client_id,
        a.depository_code,
		a.holder1_name,
        ci.insider_name,
        ci.pan_no,
		a.holder1_addr1,
		a.holder1_addr2,
		a.holder1_addr3,
		a.holder1_city,
		a.holder1_pin,
		v_nsdl_benpost_from as day1,
		v_nsdl_benpost_to as day2,
		a.share_count as day1_closing,
		ifnull(b.share_count,0) as day2_closing
	  from sta_trn_tbenpost as a
      inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	  and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	  and ci.status_flag = 'Y' and ci.delete_flag = 'N'
	  left join
	  (
		select
		  a.comp_gid,
		  a.isin_id,
		  a.dp_id,
		  a.client_id,
		  a.share_count
		from sta_trn_tbenpost as a
        inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	    and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	    and ci.status_flag = 'Y' and ci.delete_flag = 'N'
		where a.comp_gid = in_comp_gid
		and a.benpost_date = v_nsdl_benpost_to
        and a.depository_code = 'N'
		and a.delete_flag = 'N'
	  ) as b on a.comp_gid = b.comp_gid
	  and a.client_id = b.client_id
	  and a.dp_id = b.dp_id
	  and a.isin_id = b.isin_id
	  where a.comp_gid = in_comp_gid
	  and a.benpost_date = v_nsdl_benpost_from
      and a.depository_code = 'N'
	  and a.delete_flag = 'N'
      
	  union

	  select
		a.isin_id,
		a.dp_id,
		a.client_id,
		a.depository_code,
		a.holder1_name,
        ci.insider_name,
        ci.pan_no,
		a.holder1_addr1,
		a.holder1_addr2,
		a.holder1_addr3,
		a.holder1_city,
		a.holder1_pin,
		v_nsdl_benpost_from as day1,
		v_nsdl_benpost_to as day2,
		ifnull(b.share_count,0) as day1_closing,
		a.share_count as day2_closing
	  from sta_trn_tbenpost as a
      inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	  and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	  and ci.status_flag = 'Y' and ci.delete_flag = 'N'
	  left join
	  (
		select
		  a.comp_gid,
		  a.isin_id,
		  a.dp_id,
		  a.client_id,
		  a.share_count
		from sta_trn_tbenpost as a
        inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	    and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	    and ci.status_flag = 'Y' and ci.delete_flag = 'N'
		where a.comp_gid = in_comp_gid
		and a.benpost_date = v_nsdl_benpost_from
        and a.depository_code = 'N'
		and a.delete_flag = 'N'
	  ) as b on a.comp_gid = b.comp_gid
	  and a.client_id = b.client_id
	  and a.dp_id = b.dp_id
	  and a.isin_id = b.isin_id
	  where a.comp_gid = in_comp_gid
	  and a.benpost_date = v_nsdl_benpost_to
      and a.depository_code = 'N'
	  and b.comp_gid is null
	  and a.delete_flag = 'N'
union
	  select
		a.isin_id,
		a.dp_id,
		a.client_id,
        a.depository_code,
		a.holder1_name,
        ci.insider_name,
        ci.pan_no,
		a.holder1_addr1,
		a.holder1_addr2,
		a.holder1_addr3,
		a.holder1_city,
		a.holder1_pin,
		v_cdsl_benpost_from as day1,
		v_cdsl_benpost_to as day2,
		a.share_count as day1_closing,
		ifnull(b.share_count,0) as day2_closing
	  from sta_trn_tbenpost as a
      inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	  and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	  and ci.status_flag = 'Y' and ci.delete_flag = 'N'
	  left join
	  (
		select
		  a.comp_gid,
		  a.isin_id,
		  a.dp_id,
		  a.client_id,
		  a.share_count
		from sta_trn_tbenpost as a
        inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	    and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	    and ci.status_flag = 'Y' and ci.delete_flag = 'N'
		where a.comp_gid = in_comp_gid
		and a.benpost_date = v_cdsl_benpost_to
        and a.depository_code = 'C'
		and a.delete_flag = 'N'
	  ) as b on a.comp_gid = b.comp_gid
	  and a.client_id = b.client_id
	  and a.dp_id = b.dp_id
	  and a.isin_id = b.isin_id
	  where a.comp_gid = in_comp_gid
	  and a.benpost_date = v_cdsl_benpost_from
      and a.depository_code = 'C'
	  and a.delete_flag = 'N'
union
	  select
		a.isin_id,
		a.dp_id,
		a.client_id,
        a.depository_code,
		a.holder1_name,
        ci.insider_name,
        ci.pan_no,
		a.holder1_addr1,
		a.holder1_addr2,
		a.holder1_addr3,
		a.holder1_city,
		a.holder1_pin,
		v_cdsl_benpost_from as day1,
		v_cdsl_benpost_to as day2,
		ifnull(b.share_count,0) as day1_closing,
		a.share_count as day2_closing
	  from sta_trn_tbenpost as a
      inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	  and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	  and ci.status_flag = 'Y' and ci.delete_flag = 'N'
	  left join
	  (
		select
		  a.comp_gid,
		  a.isin_id,
		  a.dp_id,
		  a.client_id,
		  a.share_count
		from sta_trn_tbenpost as a
        inner join sta_mst_tcompinsiders as ci on a.comp_gid = ci.comp_gid 
	    and (ci.pan_no = a.holder1_pan or ci.pan_no = a.holder2_pan or ci.pan_no = a.holder3_pan)
	    and ci.status_flag = 'Y' and ci.delete_flag = 'N'
		where a.comp_gid = in_comp_gid
		and a.benpost_date = v_cdsl_benpost_from
        and a.depository_code = 'C'
		and a.delete_flag = 'N'
	  ) as b on a.comp_gid = b.comp_gid
	  and a.client_id = b.client_id
	  and a.dp_id = b.dp_id
	  and a.isin_id = b.isin_id
	  where a.comp_gid = in_comp_gid
	  and a.benpost_date = v_cdsl_benpost_to
	  and a.depository_code = 'C'
	  and b.comp_gid is null
	  and a.delete_flag = 'N') as c
      where (c.day1_closing > c.day2_closing,c.day1_closing - c.day2_closing,null)
      <> (c.day1_closing < c.day2_closing,c.day2_closing - c.day1_closing,null);
END$$
DELIMITER ;
