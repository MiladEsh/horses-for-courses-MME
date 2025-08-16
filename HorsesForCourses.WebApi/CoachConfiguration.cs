using HorsesForCourses.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorsesForCourses.WebApi;

public class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(new IdValueConverter<Coach>())
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        // builder.Navigation(c => c.AssignedCourses)
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);

        // builder.HasMany<Course>("_assignedCourses")
        //     .WithOne() 
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.AssignedCourses)
            .WithOne() // or .WithOne(c => c.Coach) 
            .OnDelete(DeleteBehavior.Restrict);

        // builder.Navigation(c => c.AssignedCourses)
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(c => c.Skills)
            .HasConversion(
                v => string.Join(',', v.Select(a => a.Value)),
                v => v.Split(',', StringSplitOptions.None).Select(a => Skill.From(a)).ToList());

        // builder.OwnsMany(c => c.Skills, sb =>
        // {
        //     sb.WithOwner().HasForeignKey("CoachId");

        //     sb.Property<string>("value")
        //       .HasColumnName("Skill")
        //       .IsRequired()
        //       .HasMaxLength(100);

        //     sb.HasKey("CoachId", "value");

        //     sb.ToTable("CoachSkills");
        // });

        builder.ToTable("Coaches");
    }
}
