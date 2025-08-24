using HorsesForCourses.Core.Domain;
using HorsesForCourses.Api.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorsesForCourses.Api;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // === Key + Id mapping ===
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(new IdValueConverter<Course>())
            .HasColumnName("Id")
            .ValueGeneratedNever();

        // === Scalars ===
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.StartDate)
            .IsRequired();

        builder.Property(c => c.EndDate)
            .IsRequired();

        builder.Property(c => c.IsConfirmed)
            .IsRequired();

        // === RequiredSkills ===
        builder.OwnsMany(c => c.RequiredSkills, sb =>
        {
            sb.WithOwner().HasForeignKey("CourseId");

            sb.Property<string>("value")
              .HasColumnName("Skill")
              .IsRequired()
              .HasMaxLength(100);

            sb.HasKey("CourseId", "value");

            sb.ToTable("CourseRequiredSkills");
        });

        // === TimeSlots: OwnsMany of TimeSlot ===
        builder.OwnsMany(c => c.TimeSlots, tsb =>
        {
            tsb.WithOwner().HasForeignKey("CourseId");
            tsb.ToTable("CourseTimeSlots");

            tsb.HasKey(t => new
            {
                t.CourseId,
                t.Day,
                t.StartHour
            });

            tsb.Property(t => t.Day)
                .HasColumnName("Day")
                .IsRequired();

            tsb.Ignore(t => t.StartHour);

            tsb.Property(t => t.Start)
                .HasColumnName("StartHour")
                .HasConversion(
                    v => v.Value,
                    v => OfficeHour.From(v))
                .IsRequired();

            tsb.Ignore(t => t.EndHour);

            tsb.Property(t => t.End)
                .HasColumnName("EndHour")
                .HasConversion(
                    v => v.Value,
                    v => OfficeHour.From(v))
                .IsRequired();
        });





        // === Coach assignment ===
        builder.HasOne(c => c.AssignedCoach)
               .WithMany() // assuming Coach doesn't have back-reference
               .HasForeignKey("AssignedCoachId")
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Courses");
    }
}
