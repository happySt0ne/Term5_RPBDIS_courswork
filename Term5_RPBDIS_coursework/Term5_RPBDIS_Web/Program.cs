using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Term5_RPBDIS_library;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ValuatingSystemContext>(options => 
    options.UseSqlServer(
        b => b.MigrationsAssembly("Term5_RPBDIS_Web")));
builder.Services.AddControllersWithViews(); // Add MVC to DI.
builder.Services.AddSession();
builder.Services.AddResponseCaching();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ValuatingSystemContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSession();
app.UseResponseCaching();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "search",
    pattern: "{controller=Search}/{action=Form1}");

CreateTableRoutes();

using (var scope = app.Services.CreateScope()) {
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    EnsureRoleExists("Admin", roleManager).Wait();
    EnsureRoleExists("User", roleManager).Wait();
}

app.Run();

void CreateTableRoutes() {
    string[] controllers = { "Achievemtnt", "Date", "Division", "Employee", "Mark", "PlannedEfficiency", "RealEfficiency" };

    foreach (var controller in controllers) {

        app.MapControllerRoute(
            name: controller,
            pattern:  $"{{controller={controller}}}/{{action=ShowTable}}");
    }
}

async Task EnsureRoleExists(string roleName, RoleManager<IdentityRole> roleManager) {
    if (!await roleManager.RoleExistsAsync(roleName)) {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}