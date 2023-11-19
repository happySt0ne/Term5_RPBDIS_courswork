using Microsoft.Extensions.Caching.Memory;
using Term5_RPBDIS_library;
using Term5_RPBDIS_mainLogic.Services;

const int CACHE_TIME_SECONDS = 264;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ValuatingSystemContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews(); // Add MVC to DI.
builder.Services.AddSession();

builder.Services.Configure<CookiePolicyOptions>(options =>
    options.CheckConsentNeeded = context => false
);

builder.Services.AddTransient<AchievementService>();
builder.Services.AddTransient<DateService>();
builder.Services.AddTransient<DivisionService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<MarkService>();
builder.Services.AddTransient<PlannedEfficiencyService>();
builder.Services.AddTransient<RealEfficiencyService>();

builder.Services.AddTransient(x => 
    new MemoryCacheEntryOptions {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CACHE_TIME_SECONDS),
    }
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Achievement",
    pattern: "{controller=Achievement}/{action=ShowTable}");
app.MapControllerRoute(
    name: "Date",
    pattern: "{controller=Date}/{action=ShowTable}");
app.MapControllerRoute(
    name: "Division",
    pattern: "{controller=Division}/{action=ShowTable}");
app.MapControllerRoute(
    name: "Employee",
    pattern: "{controller=Employee}/{action=ShowTable}");
app.MapControllerRoute(
    name: "Mark",
    pattern: "{controller=Mark}/{action=ShowTable}");
app.MapControllerRoute(
    name: "PlannedEfficiency",
    pattern: "{controller=PLannedEfficiency}/{action=ShowTable}");
app.MapControllerRoute(
    name: "RealEfficiency",
    pattern: "{controller=RealEfficiency}/{action=ShowTable}");

app.Run();