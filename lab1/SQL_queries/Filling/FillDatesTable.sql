DECLARE @Counter int = 1;
DECLARE @MaxRecordsNumber int = 500;

WHILE @Counter <= @MaxRecordsNumber
BEGIN
    DECLARE @StartDate DATE = DATEADD(DAY, ROUND(RAND() * 365, 0), '2019-01-01');
    DECLARE @DaysToAdd INT = ROUND(RAND() * 90, 0);
    DECLARE @EndDate DATE = DATEADD(DAY, @DaysToAdd, @StartDate);

    INSERT INTO Dates (StartDate, EndDate)
    VALUES (@StartDate, @EndDate);

    SET @Counter = @Counter + 1;
END