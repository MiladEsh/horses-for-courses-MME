namespace HorsesForCourses.Api.Courses.CreateCoach;

public record CreateCourseRequest(string Name, DateOnly StartDate, DateOnly EndDate);
