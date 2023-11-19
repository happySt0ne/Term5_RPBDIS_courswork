using Term5_RPBDIS_library;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ValuatingSystemContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews(); // Add MVC to DI.
builder.Services.AddSession();
builder.Services.AddResponseCaching();

builder.Services.Configure<CookiePolicyOptions>(options =>
    options.CheckConsentNeeded = context => false
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseResponseCaching();
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