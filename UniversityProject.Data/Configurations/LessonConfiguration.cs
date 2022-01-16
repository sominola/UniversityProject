using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Configurations;

public class LessonConfiguration: IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lesson");
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
        builder.HasMany(e => e.Users).WithMany(x => x.Lessons).UsingEntity(j=>j.ToTable("UserLesson"));
    }
}