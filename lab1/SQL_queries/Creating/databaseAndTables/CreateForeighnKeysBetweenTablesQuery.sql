USE Term5_RPBDIS_Coursework

ALTER TABLE dbo.Employees ADD FOREIGN KEY (DivisionID)
REFERENCES dbo.Divisions (DivisionID) 

ALTER TABLE dbo.Employees ADD FOREIGN KEY (AchievementID)
REFERENCES dbo.Achievements (AchievementID) 

ALTER TABLE dbo.Employees ADD FOREIGN KEY (MarkID)
REFERENCES dbo.Marks (MarkID) 

ALTER TABLE dbo.Divisions ADD FOREIGN KEY (MarkID)
REFERENCES dbo.Marks (MarkID) 

ALTER TABLE dbo.Divisions ADD FOREIGN KEY (PlannedEfficiencyID)
REFERENCES dbo.PlannedEfficiencies (PlannedEfficiencyID) 

ALTER TABLE dbo.Divisions ADD FOREIGN KEY (RealEfficiencyID)
REFERENCES dbo.RealEfficiencies (RealEfficiencyID)

ALTER TABLE dbo.PlannedEfficiencies ADD FOREIGN KEY (DateID)
REFERENCES dbo.Dates (DateID)

ALTER TABLE dbo.RealEfficiencies ADD FOREIGN KEY (DateID)
REFERENCES dbo.Dates (DateID)
