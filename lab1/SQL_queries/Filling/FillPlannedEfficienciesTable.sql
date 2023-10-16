DECLARE @Counter int = 1;
DECLARE @MaxRecords int =500;

DECLARE @Efficiecy int = 0;

WHILE @Counter <= @MaxRecords
BEGIN
	SET @Efficiecy = RAND() * 100;
	
	INSERT INTO PlannedEfficiencies (DateID, Efficiecy)
	VALUES (@Counter, @Efficiecy);

	SET @Counter = @Counter + 1;
END