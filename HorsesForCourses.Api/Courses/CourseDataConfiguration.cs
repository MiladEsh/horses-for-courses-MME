using HorsesForCourses.Core.Domain;
using HorsesForCourses.Api.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HorsesForCourses.Core.Domain.Courses;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HorsesForCourses.Api.Courses;

public class CourseDataConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        var idProp = builder.Property(c => c.Id)
            .HasConversion(new IdValueConverter<Course>())
            .Metadata;
        idProp.SetValueComparer(new IdValueComparer<Course>());
        idProp.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        idProp.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("INTEGER")
            .HasAnnotation("Sqlite:Autoincrement", true);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.StartDate)
            .IsRequired();

        builder.Property(c => c.EndDate)
            .IsRequired();

        builder.Property(c => c.IsConfirmed)
            .IsRequired();

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

        builder.OwnsMany(c => c.TimeSlots, tsb =>
        {
            tsb.WithOwner().HasForeignKey("CourseId");
            tsb.ToTable("CourseTimeSlots");

            // Composite key on FK + Day + Start (converted)
            tsb.HasKey("CourseId", nameof(TimeSlot.Day), nameof(TimeSlot.Start));

            tsb.Property(t => t.Day)
                .HasColumnName("Day")
                .IsRequired();

            // map value object -> int
            tsb.Property(t => t.Start)
                .HasColumnName("StartHour")
                .HasConversion(v => v.Value, v => OfficeHour.From(v))
                .IsRequired();

            tsb.Property(t => t.End)
                .HasColumnName("EndHour")
                .HasConversion(v => v.Value, v => OfficeHour.From(v))
                .IsRequired();

            // Make sure you do NOT ignore members used in the key
            // Remove these if you had them:
            tsb.Ignore(t => t.StartHour);
            tsb.Ignore(t => t.EndHour);
        });





        // === Coach assignment ===
        builder.HasOne(c => c.AssignedCoach)
               .WithMany() // assuming Coach doesn't have back-reference
               .HasForeignKey("AssignedCoachId")
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Courses");
    }
}
