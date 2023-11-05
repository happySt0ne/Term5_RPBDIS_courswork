using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_mainLogic;
using Term5_RPBDIS_mainLogic.Services;
using Term5_RPBDIS_sql_library;

const int CACHE_TIME_SECONDS = 264;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ValuatingSystemContext>(options => options.UseSqlServer(connection));
builder.Services.AddDistributedMemoryCache();

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Map("/info", Middlewares.GetClientInfo);
app.Map("/Achievement", Middlewares.ShowAchievement);
app.Map("/Date", Middlewares.GetClientInfo);
app.Map("/Division", Middlewares.GetClientInfo);
app.Map("/Employee", Middlewares.GetClientInfo);
app.Map("/Mark", Middlewares.GetClientInfo);
app.Map("/PlannedEfficiency", Middlewares.GetClientInfo);
app.Map("/RealEfficiency", Middlewares.GetClientInfo);

app.Run();