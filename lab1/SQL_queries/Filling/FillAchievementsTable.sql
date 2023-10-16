DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 500;

WHILE @Counter <= @MaxRecords
BEGIN
	DECLARE @AchievementText NVARCHAR(100) = 'Achievement' + CAST(@Counter as NVARCHAR(10));

	INSERT INTO  Achievements (Text)
	VALUES (@AchievementText);

	SET @Counter = @Counter + 1;
END