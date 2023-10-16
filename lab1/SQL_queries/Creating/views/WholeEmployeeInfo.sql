CREATE VIEW WholeEmployeeInfo
AS
SELECT 
	Employees.[Name] AS EmployeeName,
	Divisions.[Name] AS DivisionName,
	Employees.HireDate AS HireDate,
	Achievements.[Text] AS Achivements,
	Marks.[Value] AS Mark
FROM Employees
JOIN Divisions ON Employees.DivisionID = Divisions.DivisionID
JOIN Achievements ON Employees.AchievementID = Achievements.AchievementID
JOIN Marks ON Employees.MarkID = Marks.MarkID