USE Term5_RPBDIS_Coursework

CREATE TABLE dbo.Divisions (
	DivisionID int IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
	[Name] nvarchar(30), 
	MarkID int, 
	PlannedEfficiencyID int, 
	RealEfficiencyID int 
)

CREATE TABLE dbo.Employees (
	EmployeeID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] nvarchar(30),
	DivisionID int,
	HireDate date,
	AchievementID int,
	MarkID int
)

CREATE TABLE dbo.Achievements (
	AchievementID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Text] nvarchar(MAX)
)

CREATE TABLE dbo.Marks (
	MarkID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Value] int
)

CREATE TABLE dbo.Dates(
	DateID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	StartDate date,
	EndDate date
)

CREATE TABLE dbo.PlannedEfficiencies(
	PlannedEfficiencyID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	DateID int,
	Efficiecy int
) 

CREATE TABLE dbo.RealEfficiencies(
	RealEfficiencyID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	DateID int,
	Efficiecy int
)