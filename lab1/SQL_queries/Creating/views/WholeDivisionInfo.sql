CREATE VIEW WholeDivisionInfo 
AS
SELECT
	 Divisions.[Name] AS DivisionName,
	 Marks.[Value] AS Mark,
	 PlannedEfficiencies.Efficiecy AS PlannedEfficiency,
	 RealEfficiencies.Efficiecy AS RealEfficiency,
	 Dates.StartDate AS StartDate,
	 Dates.EndDate AS EndDate
FROM Divisions 
JOIN Marks ON Divisions.MarkID = Marks.MarkID
JOIN PlannedEfficiencies ON Divisions.PlannedEfficiencyID = PlannedEfficiencies.PlannedEfficiencyID
JOIN RealEfficiencies ON Divisions.RealEfficiencyID = RealEfficiencies.RealEfficiencyID
JOIN Dates ON RealEfficiencies.DateID = Dates.DateID