using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

class Program {
    static void Main(string[] args) {
        Task10();
    }

    private static void Task1() {
        ValuatingSystemContext context = new();

        context.Achievements
            .Select(x => new { x.AchievementId, x.Text })
            .ToList()
            .ForEach(x => Console.WriteLine($"Id = {x.AchievementId}; text = {x.Text}"));

    }

    private static void Task2() {
        ValuatingSystemContext context = new();

        context.Marks
            .Where(x => x.Value > 50)
            .Select(x => new { Id = x.MarkId, Mark = x.Value })
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static void Task3() {
        ValuatingSystemContext context = new();

        var a = context.Employees
            .OrderByDescending(x => x.Mark)
            .Select(x => new { Id = x.EmployeeId, x.Name, Mark = x.Mark.Value })
            .ToList();

        a.ForEach(Console.WriteLine);
        Console.WriteLine($"\nСредняя оценка всех работников:\n{a.Average(x => x.Mark):F2}");
    }

    private static void Task4() {
        ValuatingSystemContext context = new();

        context.Employees
            .Select(x => new { Name = x.Name, DivisionName = x.Division.Name })
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static void Task5() {
        ValuatingSystemContext context = new();

        context.Employees
            .Where(x => x.Mark.Value % 2 == 0)
            .Select(x => new { x.Name, Mark = x.Mark.Value, DivisionName = x.Division.Name })
            .ToList()
            .ForEach(Console.WriteLine);

    }

    private static void Task6() {
        ValuatingSystemContext context = new();

        var lastAchieve = context.Achievements
            .OrderBy(a => a.AchievementId)
            .Select(a => new { a.AchievementId, a.Text })
            .LastOrDefault();

        Console.WriteLine($"Last record in database before adding:\n{lastAchieve}");

        Achievement achievement = new() { Text = "compliting 6 task in lab 2)" };
        context.Achievements.Add(achievement);
        context.SaveChanges();

        lastAchieve = context.Achievements
            .OrderBy(a => a.AchievementId)
            .Select(a => new { a.AchievementId, a.Text })
            .LastOrDefault();

        Console.WriteLine($"Last record in database after adding:\n{lastAchieve}");
    }

    private static void Task7() {
        ValuatingSystemContext context = new();

        var lastRecord = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(x => new {
                x.EmployeeId,
                x.Name,
                x.HireDate,
                x.Mark.Value,
                x.Achievement.Text,
                divisionName = x.Division.Name})
            .LastOrDefault();

        Console.WriteLine($"Last record in database before adding:\n{lastRecord}");

        Employee employee = new() {
            Division = context.Divisions.FirstOrDefault(d => d.DivisionId == 4),
            HireDate = new DateTime(2023, 10, 11),
            Name = "Deniska",
            Achievement = context.Achievements.FirstOrDefault(a => a.AchievementId == 5),
            Mark = context.Marks.FirstOrDefault(m => m.MarkId == 6)
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        lastRecord = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(x => new {
                x.EmployeeId,
                x.Name,
                x.HireDate,
                x.Mark.Value,
                x.Achievement.Text,
                divisionName = x.Division.Name
            })
            .LastOrDefault();

        Console.WriteLine($"\nLast record in database after adding:\n{lastRecord}");
    }

    private static void Task8() {
        ValuatingSystemContext context = new();

        var lastRecord = context.Achievements
            .OrderBy(a => a.AchievementId)
            .LastOrDefault();
        Console.WriteLine($"Last record before deleting:\nId = {lastRecord.AchievementId}; Text = {lastRecord.Text}");

        context.Achievements.Remove(lastRecord);
        context.SaveChanges();

        lastRecord = context.Achievements
            .OrderBy(a => a.AchievementId)
            .LastOrDefault();
        Console.WriteLine($"Last record after deleting:\nId = {lastRecord.AchievementId}; Text = {lastRecord.Text}");

    }

    private static void Task9() {
        ValuatingSystemContext context = new();

        var lastRecord = context.Employees
            .OrderBy(e => e.EmployeeId)
            .LastOrDefault();

        Console.WriteLine("Last record in database before deleting:\nId=" +
            lastRecord.EmployeeId + " Name = " + lastRecord.Name);

        context.Employees.Remove(lastRecord);
        context.SaveChanges();

        lastRecord = context.Employees
            .OrderBy(e => e.EmployeeId)
            .LastOrDefault();

        Console.WriteLine("\nLast record in database after deleting:\nId=" +
            lastRecord.EmployeeId + " Name = " + lastRecord.Name);
    }

    private static void Task10() {
        ValuatingSystemContext context = new();

        var listToUpdate = context.Achievements.Where(x => x.AchievementId % 2 == 0);

        if (listToUpdate is null) return;

        foreach (var item in listToUpdate) {
            item.Text = item.Text + "☺";
        }

        context.SaveChanges();

        context.Achievements
            .Where(x => x.AchievementId % 2 == 0)
            .Select(x => new { x.AchievementId, x.Text })
            .ToList()
            .ForEach(Console.WriteLine);
    }
}