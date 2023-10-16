CREATE PROCEDURE CreateEmployee
	@Name NVARCHAR(30),
	@DivisionName NVARCHAR(30),
	@HireDate DATE,
	@AchievementText NVARCHAR(MAX),
	@Mark INT,
	@InsertedID INT OUTPUT
AS
BEGIN
	DECLARE @MarkID INT = (SELECT MarkID FROM Marks WHERE @Mark = [Value]); 
	DECLARE @DivisionID INT = (SELECT DivisionID FROM Divisions WHERE @DivisionName = [Name]);
	DECLARE @AchievementId INT = (SELECT AchievementID FROM Achievements WHERE @AchievementText = [Text]);

	IF @AchievementId IS NULL
		EXEC CreateNewAchievement @AchievementText, @AchievementId

	INSERT INTO Employees ([Name], DivisionID, HireDate, AchievementID, MarkID)
	VALUES (@Name, @DivisionID, @HireDate, @AchievementId, @MarkID);

	SET @InsertedID = SCOPE_IDENTITY();
END