using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Api.Courses.UpdateTimeSlots;

public record TimeSlotRequest(CourseDay Day, int Start, int End);