CREATE VIEW CompareRealPlannedEfficiencies AS
SELECT
	Divisions.[Name] AS DivisionName,
	RealEfficiencies.Efficiecy AS RealEfficiecy, 
	PlannedEfficiencies.Efficiecy AS PlannedEfficiecy, 
	Dates.StartDate AS StartDate, 
	Dates.EndDate AS EndDate
FROM Divisions 
JOIN RealEfficiencies ON Divisions.RealEfficiencyID = RealEfficiencies.RealEfficiencyID
JOIN PlannedEfficiencies ON Divisions.PlannedEfficiencyID = PlannedEfficiencies.PlannedEfficiencyID
JOIN Dates ON RealEfficiencies.DateID = Dates.DateID;