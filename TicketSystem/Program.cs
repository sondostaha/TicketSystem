using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TicketSystem.Controllers;
using TicketSystem.Data;
using TicketSystem.Data.Models;
using TicketSystem.Extentions;
//using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>
    (b => b.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlConn")));
builder.Services.AddIdentity<Users,IdentityRole>(ob =>
{
    ob.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    ob.Lockout.MaxFailedAccessAttempts = 5;
    ob.Lockout.AllowedForNewUsers = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7189", "http://localhost:5256")
        //policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
        
    });
});
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddTransient<IUserData, GetUserData>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustoemSwagger();

builder.Services.AddCustomJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
var app = builder.Build();

// Get the environment information

var environment = app.Environment;

// Configure the HTTP request pipeline.
if (environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseMiddleware<CustomRateLimitMiddleware>();


app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
