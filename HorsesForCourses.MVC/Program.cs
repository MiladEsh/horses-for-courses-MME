using HorsesForCourses.Service.Warehouse;
using HorsesForCourses.Service.Coaches;
using HorsesForCourses.Service.Courses;
using HorsesForCourses.Service.Coaches.GetCoaches;
using HorsesForCourses.Service.Coaches.GetCoachDetail;
using HorsesForCourses.Service.Courses.GetCourses;
using HorsesForCourses.Service.Courses.GetCourseDetail;
using Microsoft.EntityFrameworkCore;
using HorsesForCourses.MVC.Controllers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=horsesforcourses.db"));

builder.Services
    .AddScoped<IAmASuperVisor, DataSupervisor>()

    .AddScoped<IGetCoachById, GetCoachById>()
    .AddScoped<IGetCoachSummaries, GetCoachSummaries>()
    .AddScoped<IGetTheCoachDetail, GetCoachDetail>()

    .AddScoped<IGetCourseById, GetCourseById>()
    .AddScoped<IGetTheCourseSummaries, GetCourseSummaries>()
    .AddScoped<IGetTheCourseDetail, GetCourseDetail>()

    .AddScoped<CoachesRepository>()
    .AddScoped<CoursesRepository>()

    .AddScoped<CoachesService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();