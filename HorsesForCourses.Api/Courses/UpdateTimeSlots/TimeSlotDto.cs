using HorsesForCourses.Core.Domain;

namespace HorsesForCourses.Api.Courses.UpdateTimeSlots;

public record TimeSlotDto(CourseDay Day, int Start, int End);