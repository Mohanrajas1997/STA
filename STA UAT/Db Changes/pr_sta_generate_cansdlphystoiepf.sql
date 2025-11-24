DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_sta_generate_cansdlphystoiepf`(
  in in_comp_gid int,
  in in_upload_type int,
  in in_action_by varchar(16),
  out out_upload_gid int,
  out out_result int,
  out out_msg text
)
me:BEGIN
  declare err_msg text default '';
  declare err_flag boolean default false;

  declare v_comp_gid int default 0;
  declare v_entity_gid int default 0;
  declare v_upload_sno int default 0;
  declare v_tran_code char(2) default '';
  declare v_file_name varchar(64) default '';
  declare v_file_extension varchar(8) default 'txt';
  
  declare v_nsdl_count int default 0;
  declare v_nsdl_sno int default 0;

  declare v_inward_inex_status tinyint default 0;
  declare v_upload_done_status tinyint default 0;
  declare v_count int default 0;
  declare v_inward_gid int default 0;
  declare v_upload_gid int default 0;
  
  declare done int default 0;

  declare nsdl_csr cursor for
    select distinct b.inward_gid from sta_trn_tqueue as a
    inner join sta_trn_tinward as b on b.inward_gid = a.inward_gid and b.queue_gid = a.queue_gid and b.delete_flag = 'N'
    inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N'
    inner join sta_mst_tgroup as d on d.group_code = a.queue_from and c.delete_flag = 'N'
    inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N'
    inner join sta_trn_tfolio as f on b.folio_gid = f.folio_gid and f.folio_no = '00999999'
    and f.delete_flag = 'N'
    where b.comp_gid = in_comp_gid
    and
    (
      (a.queue_to = 'U')
      or
      (
        a.queue_to = 'D'
        and b.inward_all_status &
        (
          select status_value from sta_mst_tstatus
          where status_type = 'Inward'
          and status_desc = 'Inex'
          and delete_flag = 'N'
        ) > 0
      )
    )
    and a.action_status = 0
    and b.upload_gid = 0
    and a.delete_flag = 'N';

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;

 /* DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;

    set out_upload_gid = v_upload_gid;
    set out_result = 0;
    set out_msg = 'SQLEXCEPTION';
  END;*/
  
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE,
    @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
    SET @full_error = CONCAT("ERROR ", @errno, " (", @sqlstate, "): ", @text);

    ROLLBACK;
    set out_result = 0;
    set out_msg = @full_error;
  END;

  select status_value into v_inward_inex_status from sta_mst_tstatus
  where status_type = 'Inward'
  and status_desc = 'Inex'
  and delete_flag = 'N';

  set v_inward_inex_status := ifnull(v_inward_inex_status,0);

  if v_inward_inex_status = 0 then
    set err_msg := concat(err_msg,'Inward inex status to be maintained,');
    set err_flag := true;
  end if;


  select status_value into v_upload_done_status from sta_mst_tstatus
  where status_type = 'Upload'
  and status_desc = 'Upload Done'
  and delete_flag = 'N';

  set v_upload_done_status := ifnull(v_upload_done_status,0);

  if v_upload_done_status = 0 then
    set err_msg := concat(err_msg,'Upload done status to be maintained,');
    set err_flag := true;
  end if;

  
  select count(*) into v_count from sta_trn_tqueue as a
  inner join sta_trn_tinward as b on b.inward_gid = a.inward_gid and b.queue_gid = a.queue_gid and b.delete_flag = 'N'
  inner join sta_mst_ttrantype as c on c.trantype_code = b.tran_code and c.delete_flag = 'N'
  inner join sta_mst_tgroup as d on d.group_code = a.queue_from and c.delete_flag = 'N'
  inner join sta_mst_tcompany as e on e.comp_gid = b.comp_gid and e.delete_flag = 'N'
  where b.comp_gid = in_comp_gid
  and ((a.queue_to = 'U') 
  or  (a.queue_to = 'D'
  and b.inward_all_status & (128) > 0))
  -- and c.demat_flag = 'Y'
  and a.action_status = 0
  and b.upload_gid = 0
  and a.delete_flag = 'N';

  set v_count = ifnull(v_count,0);

  if v_count = 0 then
    set err_msg := concat(err_msg,'No record found,');
    set err_flag := true;
  end if;

  if exists(select upload_gid from sta_trn_tupload
      where comp_gid = in_comp_gid
      and upload_type = in_upload_type
      and upload_status = v_upload_done_status
      and delete_flag = 'N') then
    set err_msg := concat(err_msg,'Previous upload status not yet updated,');
    set err_flag := true;
  end if;

  if err_flag = false then
		set v_file_name = 'SHRI001_SCAFORIEPF_';
		set v_file_name = concat(v_file_name,DATE_FORMAT(NOW(), '%Y%m%d_%H%i%s'),'.',v_file_extension);
  end if;

  if err_flag = false then

    insert into sta_trn_tupload
    (
      comp_gid,upload_type,upload_no,upload_date,upload_by,upload_status,
      upload_filename,upload_filename_extension
    )
    values
    (
      in_comp_gid,in_upload_type,v_upload_sno,sysdate(),in_action_by,v_upload_done_status,
      v_file_name,v_file_extension
    );

    select max(upload_gid) into v_upload_gid from sta_trn_tupload;
    
    update sta_mst_tcompany
    set upload_sno = upload_sno + 1
    where comp_gid = in_comp_gid
    and delete_flag = 'N';
    
    set done = 0;

    open nsdl_csr;

    nsdl_loop:loop
      fetch nsdl_csr into v_inward_gid;

      if done = 1 then
        leave nsdl_loop;
      end if;

      update sta_trn_tinward set
        upload_gid = v_upload_gid
      where inward_gid = v_inward_gid
      and upload_gid = 0
      and delete_flag = 'N';

      set v_nsdl_count = v_nsdl_count + 1;
    end loop nsdl_loop;

    close nsdl_csr;

    if v_nsdl_count > 0 then
      
      update sta_trn_tupload set
        nsdl_sno = v_nsdl_sno
      where upload_gid = v_upload_gid
      and delete_flag = 'N';

      
      /*update sta_mst_tentity set
        cdsl_sno = cdsl_sno + 1
      where entity_gid = v_entity_gid
      and delete_flag = 'N';*/
    end if;
  else
    set out_upload_gid = v_upload_gid;
    set out_result = 0;
    set out_msg = err_msg;
    leave me;
  end if;

  set out_upload_gid = v_upload_gid;
  set out_msg = 'Upload generated successfully !';
  set out_result = 1;
END$$
DELIMITER ;
