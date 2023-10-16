DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 100;

WHILE @Counter <= @MaxRecords
BEGIN
	
	INSERT INTO Marks (Value)
	VALUES (@Counter);

	SET @Counter = @Counter + 1;
END