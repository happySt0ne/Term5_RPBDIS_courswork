DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 500;

DECLARE @Efficiecy int = 0; 

WHILE @Counter <= @MaxRecords
BEGIN
    Set @Efficiecy = RAND() * 100;

    INSERT INTO RealEfficiencies(DateID, Efficiecy)
    VALUES (@Counter, @Efficiecy);
 
    SET @Counter = @Counter + 1;
END