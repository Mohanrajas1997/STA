CREATE DEFINER=`root`@`%` EVENT `dependentscheduler_event` ON SCHEDULE EVERY 1 MINUTE STARTS '2025-01-07 13:15:00' ON COMPLETION NOT PRESERVE ENABLE DO BEGIN
    
    declare v_scheduler_gid int;
    declare v_pipeline_code varchar(32);
    
    select scheduler_gid,pipeline_code 
    into   v_scheduler_gid,v_pipeline_code
    from con_trn_tscheduler
    where scheduler_status = 'Completed'
    -- and 1 = 2
    order by 1 desc limit 1;
    
    update con_trn_tscheduler set scheduler_status = 'Ratified'
    where scheduler_gid = v_scheduler_gid
    and pipeline_code = v_pipeline_code;
    
    call pr_con_dependentscheduler(v_pipeline_code,v_scheduler_gid);
END