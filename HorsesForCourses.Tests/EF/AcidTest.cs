// using HorsesForCourses.Core.Abstractions;
// using HorsesForCourses.Core.Domain;
// using HorsesForCourses.Core.Domain.Coaches;
// using HorsesForCourses.WebApi;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using QuickAcid;
// using QuickFuzzr;
// using QuickFuzzr.Data;
// using QuickFuzzr.UnderTheHood;
// using QuickPulse.Investigates;
// using QuickPulse.Show;
// using WibblyWobbly;

// namespace HorsesForCourses.Tests.EF;

// public class AcidTest
// {

//     [Fact]
//     public void Shoot()
//     {
//         var script =

//             from options in "Options".Stashed(() =>
//             {
//                 var connection = new SqliteConnection("DataSource=:memory:");
//                 connection.Open();
//                 var options = new DbContextOptionsBuilder<AppDBContext>()
//                     .UseSqlite(connection)
//                     .Options;
//                 using var context = GetDbContext(options);
//                 context.Database.EnsureCreated();
//                 return options;
//             })

//             from coach in "Coach".Input(CoachGenerator)
//             from create in "Create".Act(() =>
//             {
//                 using var context = GetDbContext(options);
//                 context.Add(coach);
//                 context.SaveChanges();
//             })

//             from createdReloaded in "CreatedReloaded".Act(() =>
//             {
//                 using var context = GetDbContext(options);
//                 return context.Coaches.AsNoTracking().Single(a => a.Id == coach.Id);
//             })
//             from createNotNull in "Reloaded Coach is not Null".Spec(() => createdReloaded != null)
//             from createSpec in "Create and then reload is memberwise equal.".Spec(
//                 () => Investigate.These(coach, createdReloaded).AllEqual)


//             from newSkill in "New Skill".Input(
//                 Fuzz.ChooseFromThese(Skills)
//                     .Where(a => !createdReloaded.Skills.Contains(Skill.From(a)))
//                     .Many(0, 3))
//             from update in "Update".Act(() =>
//             {
//                 using var context = GetDbContext(options);
//                 var toUpdate = context.Coaches.Single(a => a.Id == coach.Id);
//                 // change coach, only skills available
//                 newSkill.ForEach(a => toUpdate.AddSkill(Skill.From(a)));
//                 //--
//                 context.SaveChanges();
//                 return toUpdate;
//             })

//             from updatedReloaded in "UpdatedReloaded".Act(() =>
//             {
//                 using var context = GetDbContext(options);
//                 return context.Coaches.AsNoTracking().Single(a => a.Id == coach.Id);
//             })
//             from updateNotNull in "Updated Coach is not Null".Spec(() => updatedReloaded != null)
//                 // from updateSkills in "Update and then reload new skill added.".SpecIf(
//                 //     () => updatedReloaded != null && !string.IsNullOrEmpty(newSkill),
//                 //     () => updatedReloaded.Skills.Contains(Skill.From(newSkill)))
//             from findings in "findings".Derived(() => Investigate.These(update, updatedReloaded))
//             from trace in "Findings: ".TraceIf(() => !findings.AllEqual, () => Introduce.This(findings.TheExhibit))
//             from updateSpec in "Update and then reload is memberwise equal.".Spec(
//                 () => findings.AllEqual)

//             select Acid.Test;

//         QState.Run(script)
//             .Options(a => a with
//             {
//                 // ShrinkingActions = true,
//                 FileAs = "Acid",
//                 // Verbose = true
//             })
//             .With(100.Runs()).AndOneExecutionPerRun();
//     }

//     private static AppDBContext GetDbContext(DbContextOptions<AppDBContext> options)
//     {
//         var context = new AppDBContext(options);
//         return context;
//     }

//     private readonly static string[] Skills =
//         [ "C#"
//         , "Cookery"
//         , "Agile Dev"
//         , "Kung Fu"
//         , "Flower Arranging"];

//     private static readonly Generator<Coach> CoachGenerator =
//         from firstname in Fuzz.ChooseFrom(DataLists.FirstNames)
//         from lastname in Fuzz.ChooseFrom(DataLists.LastNames)
//         let name = $"{firstname} {lastname}"
//         let email = $"{firstname}.{lastname}@mail.com"
//         from skill in Fuzz.ChooseFromThese(Skills)
//         from coach in Fuzz.Constant(new Coach(name, email))
//             .Apply(a => a.AddSkill(Skill.From(skill)))
//         select coach;

//     private readonly static string[] CourseSuffixes =
//         ["For Dummies", "Basic Techiques", "Advanced", "101", "For Professionals", "The Ultimate", "What You Always Wanted to Know"];

//     private static Generator<TimeSlot> TimeslotGeneratorFor(int key) =>
//         from start in Fuzz.Int(9, 17)
//         from end in Fuzz.Int(start + 1, 18)
//         from day in Fuzz.Enum<CourseDay>().Unique(key)
//         from timeslot in Fuzz.Constant(TimeSlot.From(day, OfficeHour.From(start), OfficeHour.From(end)))
//         select timeslot;

//     private static readonly Generator<Course> CourseGenerator =
//         from start in Fuzz.Int(1, 31)
//         let startDate = DateOnly.FromDateTime(start.January(2025))
//         from end in Fuzz.Int(start + 1, 32)
//         let endDate = DateOnly.FromDateTime(end.January(2025))
//         from skill in Fuzz.ChooseFromThese(Skills)
//         from namePartTwo in Fuzz.ChooseFromThese(CourseSuffixes)
//         let name = $"{skill}, {namePartTwo}."
//         from key in Fuzz.Int().Unique("weekday-key")
//         from timeslots in TimeslotGeneratorFor(key).Many(1, 5)
//         from course in Fuzz.Constant(new Course(name, startDate, endDate))
//             .Apply(a => a.AddRequiredSkill(Skill.From(skill)))
//             .Apply(a => timeslots.OrderBy(c => c.Day).ForEach(b => a.AddTimeSlot(b)))
//         select course;

//     [Fact(Skip = "debug")]
//     public void TryIt()
//     {
//         CoachGenerator.Generate().PulseToLog();
//     }
// }