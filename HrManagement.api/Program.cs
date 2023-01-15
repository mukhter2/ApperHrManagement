using HrManagement.context;
using HrManagement.core;
using HrManagement.context.efcore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureDatabase(configuration);
builder.Services.ConfigureServices();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
//
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();
