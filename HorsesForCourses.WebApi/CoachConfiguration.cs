using HorsesForCourses.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorsesForCourses.WebApi;

public class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        // === Key & ID mapping ===
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(new IdValueConverter<Coach>())
            .HasColumnName("Id")
            .ValueGeneratedNever();

        // === Scalar properties ===
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        // === AssignedCourses ===
        // builder.Navigation(c => c.AssignedCourses)
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);

        // builder.HasMany<Course>("_assignedCourses")
        //     .WithOne() // adjust if Course has a Coach navigation
        //     .OnDelete(DeleteBehavior.Restrict);

        // === Skills: OwnsMany value object ===
        builder.OwnsMany(c => c.Skills, sb =>
        {
            sb.WithOwner().HasForeignKey("CoachId");

            sb.Property<string>("value")
              .HasColumnName("Skill")
              .IsRequired()
              .HasMaxLength(100);

            sb.HasKey("CoachId", "value");

            sb.ToTable("CoachSkills");
        });

        builder.ToTable("Coaches");
    }
}
