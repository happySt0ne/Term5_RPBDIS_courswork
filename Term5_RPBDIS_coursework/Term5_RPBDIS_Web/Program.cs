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

//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ValuatingSystemContext>(/*options => options.UseSqlServer(connection)*/);
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

app.Map("/info", Middlewares.Info.ShowClientInfo);
app.Map("/Achievement", Middlewares.Tables.ShowAchievement);
app.Map("/Date", Middlewares.Tables.ShowDate);
app.Map("/Division", Middlewares.Tables.ShowDivision);
app.Map("/Employee", Middlewares.Tables.ShowEmployee);
app.Map("/Mark", Middlewares.Tables.ShowMark);
app.Map("/PlannedEfficiency", Middlewares.Tables.ShowPlannedEfficiency);
app.Map("/RealEfficiency", Middlewares.Tables.ShowRealEfficiency);
app.Map("/searchform1", Middlewares.Search.ShowForm1);
app.Map("/searchform2", Middlewares.Search.ShowForm2);

app.Run();