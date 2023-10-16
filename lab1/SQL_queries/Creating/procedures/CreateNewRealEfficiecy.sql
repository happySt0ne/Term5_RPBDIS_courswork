CREATE PROCEDURE CreateNewRealEfficiecy
	@Efficiecy INT,
	@StartDate DATE,
	@EndDate DATE,
	@InsertedID INT OUTPUT
AS
BEGIN
	DECLARE @DateId INT;

	IF EXISTS (
		SELECT 1
		FROM Dates
		WHERE StartDate = @StartDate AND EndDate = @EndDate
	)
	BEGIN 
		SELECT @DateId = DateID
		FROM Dates
		WHERE StartDate = @StartDate AND EndDate = @EndDate;
	END
	ELSE 
	BEGIN
		EXEC CreateNewDate @StartDate, @EndDate, @DateId;
	END

	INSERT INTO RealEfficiencies(Efficiecy, DateID)
	VALUES (@Efficiecy, @DateId);

	SET @InsertedID = SCOPE_IDENTITY();
END