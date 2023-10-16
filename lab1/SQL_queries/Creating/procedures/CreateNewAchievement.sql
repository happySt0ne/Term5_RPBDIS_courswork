CREATE PROCEDURE CreateNewAchievement
	@Text NVARCHAR(MAX),
	@InsertedId INT OUTPUT
AS
BEGIN
	INSERT INTO Achievements([Text])
	VALUES (@Text);

	SET @InsertedId = SCOPE_IDENTITY();
END