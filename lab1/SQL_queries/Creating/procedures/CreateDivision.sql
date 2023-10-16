CREATE PROCEDURE CreateDivision
	@Name NVARCHAR(100),
	@Mark INT,
	@StartDate DATE,
	@EndDate DATE,
	@PlannedEfficiecy INT,
	@RealEfficiecy INT,
	@InsertedID INT OUTPUT
AS
BEGIN
	DECLARE @MarkID INT = (SELECT MarkID FROM Marks WHERE [Value] = @Mark);
	DECLARE @PlannedID INT;
	DECLARE @RealID INT;

	SELECT @PlannedID = PlannedEfficiencyID
	FROM PlannedEfficiencies
	JOIN Dates ON PlannedEfficiencies.DateID = Dates.DateID
	WHERE @StartDate = Dates.StartDate AND @EndDate = Dates.EndDate AND @PlannedEfficiecy = PlannedEfficiencies.Efficiecy
	
	SELECT @RealID = RealEfficiencyID
	FROM RealEfficiencies
	JOIN Dates ON RealEfficiencies.DateID = Dates.DateID
	WHERE @StartDate = Dates.StartDate AND @EndDate = Dates.EndDate AND @RealEfficiecy = RealEfficiencies.Efficiecy
	
	IF @PlannedID IS NULL
		EXEC CreateNewPlannedEfficiecy @PlannedEfficiecy, @StartDate, @EndDate, @PlannedID

	IF @RealID IS NULL
		EXEC CreateNewRealEfficiecy @RealEfficiecy, @StartDate, @EndDate, @RealID

	INSERT INTO Divisions([Name], MarkID, PlannedEfficiencyID, RealEfficiencyID)
	VALUES (@Name, @MarkID, @PlannedID, @RealID);

	SET @InsertedID = SCOPE_IDENTITY();
END