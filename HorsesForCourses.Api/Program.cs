
using HorsesForCourses.Api.Coaches;
using HorsesForCourses.Api.Coaches.GetCoaches;
using HorsesForCourses.Api.Courses;
using HorsesForCourses.Api.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=horsesforcourses.db"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IAmASuperVisor, DataSupervisor>()

    .AddScoped<IGetCoachById, GetCoachById>()
    .AddScoped<IGetTheCoachSummaries, GetCoachSummaries>()
    .AddScoped<IGetTheCoachDetail, GetCoachDetail>()

    .AddScoped<IGetCourseById, GetCourseById>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Title = "Er is een fout opgetreden",
                Status = 500,
                Detail = "Er ging iets mis tijdens het verwerken van je verzoek."
            };

            await context.Response.WriteAsJsonAsync(problem);
        });
    });
}

app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }