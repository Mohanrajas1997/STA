DELIMITER //

CREATE FUNCTION fn_get_date_range_category(received_date DATE)
RETURNS VARCHAR(50)
DETERMINISTIC
BEGIN
    DECLARE category VARCHAR(50);
    DECLARE days_diff INT;

    SET days_diff = DATEDIFF(SYSDATE(), received_date);

    IF days_diff BETWEEN 1 AND 5 THEN
        SET category = '1-5 Days';
    ELSEIF days_diff BETWEEN 6 AND 10 THEN
        SET category = '6-10 Days';
    ELSEIF days_diff BETWEEN 11 AND 15 THEN
        SET category = '11-15 Days';
    ELSEIF days_diff BETWEEN 16 AND 21 THEN
        SET category = '16-21 Days';
    ELSEIF days_diff BETWEEN 22 AND 30 THEN
        SET category = '22-30 Days';
    ELSEIF days_diff > 30 THEN
        SET category = '>30 Days';
    ELSE
        SET category = 'Uncategorized';
    END IF;

    RETURN category;
END;
//

DELIMITER ;
