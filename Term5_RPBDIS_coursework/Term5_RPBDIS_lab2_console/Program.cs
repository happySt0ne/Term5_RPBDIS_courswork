using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Collections;
using System.Threading.Channels;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

class Program {
    static void Main(string[] args) {
        CourseworkContext context = new();
        context.Marks.ToList().ForEach(x => Console.WriteLine(x.Value));


    }

    private static void Task1() {
        CourseworkContext context = new();

        context.Achievements
            .Select(x => new { x.AchievementId, x.Text })
            .ToList()
            .ForEach(x => Console.WriteLine($"Id = {x.AchievementId}; text = {x.Text}"));
    }

    private static void Task2() {
        CourseworkContext context = new();

        context.Marks
            .Where(x => x.Value > 50)
            .Select(x => new { Id = x.MarkId, Mark = x.Value })
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static void Task3() {
        CourseworkContext context = new();

        var a = context.Employees
            .OrderByDescending(x => x.Mark)
            .Select(x => new { Id = x.EmployeeId, x.Name, Mark = x.Mark.Value })
            .ToList();

        a.ForEach(Console.WriteLine);
        Console.WriteLine($"\nСредняя оценка всех работников:\n{a.Average(x => x.Mark):F2}");
    }

    private static void Task4() {
        CourseworkContext context = new();

        context.Employees
            .Select(x => new { Name = x.Name, DivisionName = x.Division.Name })
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static void Task5() {
        CourseworkContext context = new();

        context.Employees
            .Where(x => x.Mark.Value % 2 == 0)
            .Select(x => new { x.Name, Mark = x.Mark.Value, DivisionName = x.Division.Name })
            .ToList()
            .ForEach(Console.WriteLine);

    }

    private static void Task6() {
        CourseworkContext context = new();

        Achievement achievement = new() { Text = "compliting 6 task in lab 2)" };
        context.Achievements.Add(achievement);
        context.SaveChanges();

        context.Achievements.ToList().ForEach(x => Console.WriteLine(x.Text));
    }

    private static void Task7() {
        CourseworkContext context = new();

        Employee employee = new() {
            Division = context.Divisions.FirstOrDefault(d => d.DivisionId == 4),
            HireDate = new DateTime(2023, 10, 11),
            Name = "Deniska",
            Achievement = context.Achievements.FirstOrDefault(a => a.AchievementId == 5),
            Mark = context.Marks.FirstOrDefault(m => m.MarkId == 6)
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        context.Employees
            .Select(x => new {
                x.EmployeeId,
                x.Name,
                x.HireDate,
                x.Mark.Value,
                x.Achievement.Text,
                divisionName = x.Division.Name
            })
            .OrderBy(x => x.EmployeeId)
            .ToList()
            .ForEach(Console.WriteLine);

    }
}