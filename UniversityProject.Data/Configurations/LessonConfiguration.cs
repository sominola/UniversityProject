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
        builder.HasMany(s => s.Students)
            .WithMany(c => c.Lessons).UsingEntity<LessonUser>(
                x=>x.HasOne(y=>y.Student).WithMany().HasForeignKey(z=>z.StudentId),
                x=>x.HasOne(y=>y.Lesson).WithMany().HasForeignKey(z=>z.LessonId));
        builder.HasMany(s => s.Teachers)
            .WithMany("TeacherLessons").UsingEntity<LessonTeacher>(
                x=>x.HasOne(y=>y.Teacher).WithMany().HasForeignKey(z=>z.TeacherId),
                x=>x.HasOne(y=>y.Lesson).WithMany().HasForeignKey(z=>z.LessonId));
        // builder.HasMany(x => x.Students).WithMany(x => x.Lessons).UsingEntity(j => j.ToTable("UserLessons"));
        // builder.HasMany(x=>x.Teachers).WithMany("TeacherLessons").UsingEntity(j=>j.ToTable("TeacherLessons"));
    }
}