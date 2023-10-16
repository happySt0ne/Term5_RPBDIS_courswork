CREATE PROCEDURE CreateNewDate
	@StartDate DATE,
	@EndDate DATE,
	@InsertedId INT OUTPUT
AS
BEGIN
	INSERT INTO Dates(StartDate, EndDate)
	VALUES (@StartDate, @EndDate);

	SET @InsertedId = SCOPE_IDENTITY();
END;