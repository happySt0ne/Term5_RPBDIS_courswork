DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 500;

DECLARE @Name NVARCHAR(100); 
DECLARE @Mark INT;

WHILE @Counter <= @MaxRecords
BEGIN
	
	SET @Name = 'Division name' + CAST(@Counter AS NVARCHAR(10)) ;
	SET @Mark = RAND() * 100;

	INSERT INTO Divisions ([Name], MarkID, PlannedEfficiencyID, RealEfficiencyID)
	VALUES (@Name, @Mark, @Counter, @Counter);

	SET @Counter = @Counter + 1;
END