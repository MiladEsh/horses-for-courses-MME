using HorsesForCourses.Core.Domain.Coaches;
using HorsesForCourses.Api.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HorsesForCourses.Api;

public class CoachesDataConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasKey(c => c.Id);

        var idProp = builder.Property(c => c.Id)
            .HasConversion(new IdValueConverter<Coach>())
            .Metadata;
        idProp.SetValueComparer(new IdValueComparer<Coach>());
        idProp.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        idProp.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("INTEGER")
            .HasAnnotation("Sqlite:Autoincrement", true);

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
