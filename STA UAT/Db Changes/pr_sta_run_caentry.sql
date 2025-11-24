DELIMITER $$
-- drop procedure pr_sta_run_caentry;
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_run_caentry`(in in_inward_gid int(11),
in in_comp_gid int(11))
me:BEGIN
	#created by : Mohan
	#created date : 03/09/2025
    
declare v_certno int ;
declare v_folio_gid int ;
declare v_cert_gid int ;
declare v_folio_no varchar(32) ;
declare v_certshares int default 0;

  # Get folio_gid against inward_gid
  select folio_gid into v_folio_gid 
  from sta_trn_tinward
  where inward_gid = in_inward_gid
  and delete_flag = 'N';
  
  # Get folio_no against folio_gid
  select folio_no into v_folio_no from sta_trn_tfolio 
  where folio_gid = v_folio_gid 
  and delete_flag = 'N';
  
  if v_folio_no = '00888888' then
		set v_certno = -1;
  elseif v_folio_no = '00999999' then
		set v_certno = -2;
  end if;

  # insert in cert table
  if exists (select '*' from sta_trn_tcert where comp_gid = in_comp_gid
			  and cert_no = v_certno
			  and delete_flag = 'N') then
              select cert_gid into v_cert_gid
              from sta_trn_tcert where delete_flag = 'N';
  
  # Update share count in cert table
  select sum(share_count) into v_certshares from sta_trn_tcert 
  where cert_gid = v_cert_gid and delete_flag = 'N';
  
  update sta_trn_tcert set share_count = share_count + v_certshares
  where cert_gid = v_cert_gid and delete_flag = 'N';
  else
		insert into sta_trn_tcert (comp_gid,folio_gid,cert_no,cert_sub_no,issue_date,
									share_count,cert_status,cert_remark,delete_flag)
		select in_comp_gid,v_folio_gid,v_certno,1,now(),sum(share_count),1,'CA Allotment','N'
		from sta_trn_tcaentry where inward_gid = in_inward_gid
		and delete_flag = 'N';
        
        -- get last inserted cert_gid
		set v_cert_gid = LAST_INSERT_ID();        
  end if;
  
  #insert in cert distinct table 
  insert into sta_trn_tcertdist (cert_gid,dist_from,dist_to,dist_count,delete_flag)
  select v_cert_gid,dist_from,dist_to,share_count,'N'
  from sta_trn_tcaentry where inward_gid = in_inward_gid
  and delete_flag = 'N';
								
END$$
DELIMITER ;
