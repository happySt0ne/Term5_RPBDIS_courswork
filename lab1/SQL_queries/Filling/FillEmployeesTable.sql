DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 20000;

DECLARE @Name NVARCHAR(100);
DECLARE @RandomDate DATE;
DECLARE @RandomDivision INT;
DECLARE @RandomAchievement INT;
DECLARE @RandomMark INT;

WHILE @Counter <= @MaxRecords
BEGIN
	SET @Name = 'Employee name' + CAST(@Counter AS NVARCHAR(100))
	SET @RandomDate = DATEADD(DAY, FLOOR(RAND() * (DATEDIFF(DAY, '2014-01-01', '2020-01-01') + 1)), '2014-01-01');
	SET @RandomDivision = Rand() * 500 + 1;
	SET @RandomAchievement = RAND() * 500 + 1;
	SET @RandomMark = RAND() * 100 + 1;

	INSERT INTO Employees ([Name], HireDate, DivisionID, AchievementID, MarkID)
	VALUES (@Name, @RandomDate, @RandomDivision, @RandomAchievement, @RandomMark);

	SET @Counter = @Counter + 1;
END