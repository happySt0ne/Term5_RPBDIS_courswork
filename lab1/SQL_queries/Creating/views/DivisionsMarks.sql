CREATE VIEW DivisionsMarks 
AS
SELECT 
	Divisions.[Name] AS DivisionName,
	Marks.[Value] AS Mark
FROM Divisions
JOIN Marks ON Divisions.MarkID = Marks.MarkID