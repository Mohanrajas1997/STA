DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `pr_con_dependentscheduler`(in in_pipeline_code varchar(32),in in_scheduler_gid int(11))
me:BEGIN
-- Variable Declarations
	DECLARE v_fltcondition_text TEXT;
    DECLARE v_rejcondition_text TEXT;
    DECLARE v_target_dataset_code TEXT;
	DECLARE v_table_view_query_desc TEXT;
    DECLARE v_parent_dataset TEXT;
    DECLARE v_dataset_table_field VARCHAR(32);
    DECLARE v_dataset_table_bcpfield VARCHAR(32);
    DECLARE v_done INT DEFAULT 0;
    DECLARE v_pipeline_code VARCHAR(32);
    DECLARE v_scheduler_gid INT(11);
    DECLARE start_index INT DEFAULT 1;
    DECLARE end_index INT;
    DECLARE start_index1 INT DEFAULT 1;
    DECLARE end_index1 INT;
    DECLARE extracted_text VARCHAR(255);
    DECLARE v_sql text;
    DECLARE v_bcpcol text;
    DECLARE v_trgcol text;

    -- Cursor Declaration
    DECLARE cur CURSOR FOR
        SELECT pipeline_code
        FROM con_trn_tpplfinalization
        WHERE (parent_dataset_code = (select target_dataset_code from con_mst_tpipeline
          where pipeline_code = in_pipeline_code and pipeline_status = 'Active' and delete_flag = 'N')
               OR parent_dataset_code = CONCAT((select target_dataset_code from con_mst_tpipeline
          where pipeline_code = in_pipeline_code and pipeline_status = 'Active' and delete_flag = 'N'), '_view'))
          AND delete_flag = 'N'
          AND run_type = 'Dependent';

    -- Declare NOT FOUND handler
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    -- Open the cursor
    OPEN cur;

    -- Cursor loop
    cursor_loop: LOOP
        FETCH cur INTO v_pipeline_code;
		
        select concat(db_name,'.',table_view_query_desc) into v_table_view_query_desc 
        from con_mst_tpipeline
		where pipeline_code = v_pipeline_code 
        and pipeline_status = 'Active' and delete_flag = 'N';
        
        select v_table_view_query_desc,v_pipeline_code;
        
        IF v_done THEN
            LEAVE cursor_loop;
        END IF;
        
        -- Insert logic for processing inside the cursor loop
        INSERT INTO con_trn_tscheduler 
        (scheduled_date, pipeline_code, scheduler_start_date, scheduler_status, scheduler_initiated_by, delete_flag)
        SELECT NOW(), v_pipeline_code, NOW(), 'Scheduled', 'System', 'N'
        FROM con_trn_tscheduler 
        WHERE scheduler_gid = in_scheduler_gid
		-- AND scheduler_status = 'Completed' 
        AND delete_flag = 'N' ;
        
        SELECT MAX(scheduler_gid) INTO v_scheduler_gid 
        FROM con_trn_tscheduler;
        
		#Inclusion Condition Framing part
        DROP TEMPORARY TABLE IF EXISTS tmpfiltercondition;
        CREATE TEMPORARY TABLE tmpfiltercondition(pipeline_code VARCHAR(64), 
        filter_field VARCHAR(250),filter_bcpfield VARCHAR(250));

        -- Get inclusion condition
        if exists(SELECT * FROM con_trn_tpplcondition
			WHERE pipeline_code = v_pipeline_code
			AND condition_type = 'Filter'
			AND delete_flag = 'N') then
			SELECT 
				concat(' AND ' , ifnull(condition_text,'1 = 1')) INTO v_fltcondition_text
			FROM con_trn_tpplcondition
			WHERE pipeline_code = v_pipeline_code
			AND condition_type = 'Filter'
			AND delete_flag = 'N';
        
			set v_fltcondition_text = ifnull(v_fltcondition_text,'');
        else
			set v_fltcondition_text = '';
        end if;
        
        /*select v_fltcondition_text,start_index;
        set start_index = 1;
		set v_fltcondition_text = v_fltcondition_text;
        
        -- Loop to process filter conditions
        WHILE start_index > 0 DO
            -- Find the position of "[" and "]"
			SET start_index := instr(v_fltcondition_text,'[');
            
            IF start_index > 0 THEN
                SET end_index := instr(v_fltcondition_text,']');
                
                -- Extract text between "[" and "]"
                SET extracted_text := SUBSTRING(v_fltcondition_text, start_index + 1, end_index - start_index - 1);
				set v_dataset_table_field = extracted_text;
                
                select v_dataset_table_field;
                
                SELECT dataset_table_field INTO v_dataset_table_bcpfield
                FROM con_trn_tpplsourcefield
                WHERE pipeline_code = v_pipeline_code 
                AND sourcefield_name = v_dataset_table_field
                AND delete_flag = 'N';

                -- Insert extracted text into the temporary table
                INSERT INTO tmpfiltercondition (pipeline_code,filter_field,filter_bcpfield) 
                VALUES (v_pipeline_code,v_dataset_table_field, v_dataset_table_bcpfield);
                
                select * from tmpfiltercondition;
                -- Move the start index to the position after "]"
                SET start_index := end_index + 1;
				
				set v_fltcondition_text = SUBSTR(v_fltcondition_text,start_index);
            END IF;
        END WHILE;*/
        
		/*if exists (select * from tmpfiltercondition 
					where pipeline_code = v_pipeline_code) then 
					select replace(ifnull(v_fltcondition_text,''),ifnull(filter_field,''),ifnull(filter_bcpfield,'')) 
					into v_fltcondition_text
					from tmpfiltercondition where pipeline_code = v_pipeline_code;
					
					SET v_fltcondition_text = REPLACE(v_fltcondition_text, '[', '`');
					SET v_fltcondition_text = REPLACE(v_fltcondition_text, ']', '`');
					set v_fltcondition_text = ifnull(v_fltcondition_text,'');
         end if;*/
         SET v_fltcondition_text = REPLACE(v_fltcondition_text, '[', '`');
		 SET v_fltcondition_text = REPLACE(v_fltcondition_text, ']', '`');
		 set v_fltcondition_text = ifnull(v_fltcondition_text,'');
         
         #Rejection Condition Framing part
        DROP TEMPORARY TABLE IF EXISTS tmprejectioncondition;
        CREATE TEMPORARY TABLE tmprejectioncondition(pipeline_code VARCHAR(64), 
        rejection_field VARCHAR(250),rejection_bcpfield VARCHAR(250));

        -- Get rejection condition
        if exists(SELECT * FROM con_trn_tpplcondition
			WHERE pipeline_code = v_pipeline_code
			AND condition_type = 'Rejection'
			AND delete_flag = 'N') then
			SELECT 
				concat(' AND ' , ifnull(condition_text,'1 = 1')) INTO v_rejcondition_text
			FROM con_trn_tpplcondition
			WHERE pipeline_code = v_pipeline_code
			AND condition_type = 'Rejection'
			AND delete_flag = 'N';
        
			set v_rejcondition_text = ifnull(v_rejcondition_text,'');
        else
			set v_rejcondition_text = '';
        end if;
       /* select v_rejcondition_text,start_index1;
        set start_index1 = 1;
		set v_rejcondition_text = v_rejcondition_text;
        
        -- Loop to process filter conditions
        WHILE start_index1 > 0 DO
            -- Find the position of "[" and "]"
			SET start_index1 := instr(v_rejcondition_text,'[');
            
            IF start_index1 > 0 THEN
                SET end_index1 := instr(v_rejcondition_text,']');
                
                -- Extract text between "[" and "]"
                SET extracted_text := SUBSTRING(v_rejcondition_text, start_index1 + 1, end_index1 - start_index1 - 1);
				set v_dataset_table_field = extracted_text;
                
                select v_dataset_table_field;
                
                SELECT dataset_table_field INTO v_dataset_table_bcpfield
                FROM con_trn_tpplsourcefield
                WHERE pipeline_code = v_pipeline_code 
                AND sourcefield_name = v_dataset_table_field
                AND delete_flag = 'N';

                -- Insert extracted text into the temporary table
                INSERT INTO tmprejectioncondition (pipeline_code,rejection_field,rejection_bcpfield) 
                VALUES (v_pipeline_code,v_dataset_table_field, v_dataset_table_bcpfield);
                
                select * from tmprejectioncondition;
                -- Move the start index to the position after "]"
                SET start_index1 := end_index1 + 1;
				
				set v_rejcondition_text = SUBSTR(v_rejcondition_text,start_index1);
            END IF;
        END WHILE;*/
        
		/*if exists (select * from tmprejectioncondition 
					where pipeline_code = v_pipeline_code) then 
					select replace(ifnull(v_rejcondition_text,''),ifnull(rejection_field,''),ifnull(rejection_bcpfield,'')) 
					into v_rejcondition_text
					from tmprejectioncondition where pipeline_code = v_pipeline_code;
					
					SET v_rejcondition_text = REPLACE(v_rejcondition_text, '[', '`');
					SET v_rejcondition_text = REPLACE(v_rejcondition_text, ']', '`');
					set v_rejcondition_text = ifnull(v_rejcondition_text,'');
         end if;*/
         
        SET v_rejcondition_text = REPLACE(v_rejcondition_text, '[', '`');
		SET v_rejcondition_text = REPLACE(v_rejcondition_text, ']', '`');
		set v_rejcondition_text = ifnull(v_rejcondition_text,''); 
        
        select 
			group_concat(a.dataset_table_field) ,
			group_concat(concat("`",a.sourcefield_name,"`")) into v_bcpcol,v_trgcol
		from con_trn_tpplfieldmapping as b
		left join con_trn_tpplsourcefield as a on a.pipeline_code = b.pipeline_code 
		and a.sourcefield_name = b.ppl_field_name
		and a.delete_flag = 'N'
		where b. pipeline_code =  v_pipeline_code
		and (b.pipeline_code = a.pipeline_code or b.pplfieldmapping_flag = 1) 
		and b.delete_flag = 'N' order by pplsourcefield_gid;
        
        select v_bcpcol,v_scheduler_gid,in_scheduler_gid;
        
        set v_sql = '';
	    /*set v_sql = concat('insert into con_trn_tbcp (scheduler_gid,',v_bcpcol,')');
	    set v_sql = concat(v_sql,' select ',v_scheduler_gid,',', v_bcpcol ,' from con_trn_tbcp ');
		set v_sql = concat(v_sql,' where status_flag = "V" and scheduler_gid = ',in_scheduler_gid ,v_fltcondition_text,v_rejcondition_text);*/
		
        set v_sql = concat('insert into con_trn_tbcp (scheduler_gid,',v_bcpcol,')');
	    set v_sql = concat(v_sql,' select ',v_scheduler_gid,',', v_trgcol ,' from ', v_table_view_query_desc);
		set v_sql = concat(v_sql,' where 1=1 ',v_fltcondition_text,v_rejcondition_text);
        
      -- select v_sql;  
	  set @v_sql = v_sql;
	  select cast(@v_sql as nchar);
      select v_bcpcol;
	  prepare stmt from @v_sql;
	  execute stmt;
	  deallocate prepare stmt;
      
      call pr_con_set_dataprocessing(v_pipeline_code,v_scheduler_gid);
      
    END LOOP cursor_loop;
  
    -- Close cursor
    CLOSE cur;
END$$
DELIMITER ;
