using Term5_RPBDIS_library;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ValuatingSystemContext>();
builder.Services.AddControllersWithViews(); // Add MVC to DI.
builder.Services.AddSession();
builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSession();
app.UseResponseCaching();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "search",
    pattern: "{controller=Search}/{action=Form1}");

CreateTableRoutes();

app.Run();

void CreateTableRoutes() {
    string[] controllers = { "Achievemtnt", "Date", "Division", "Employee", "Mark", "PlannedEfficiency", "RealEfficiency" };

    foreach (var controller in controllers) {
        app.MapControllerRoute(
            name: controller,
            pattern:  $"{{controller={controller}}}/{{action=ShowTable}}");
    }
}