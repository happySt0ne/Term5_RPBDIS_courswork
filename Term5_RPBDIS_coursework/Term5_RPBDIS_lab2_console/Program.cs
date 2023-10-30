using Microsoft.EntityFrameworkCore;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

class Program {
    static void Main(string[] args) {
        CourseworkContext context = new();

        var a = context.Achievements;

        foreach (var item in a) {
            Console.WriteLine(item.Text);
        }
    }
}