using Microsoft.EntityFrameworkCore;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public class StudentsTrackerDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubjectGrade> Grades { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }


    public StudentsTrackerDbContext(DbContextOptions<StudentsTrackerDbContext> options) 
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseSqlite("Data Source=stracker.db");
            //.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // indexes
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.IdCard)
            .IsUnique();

        // relationships - navigation properties
        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.Subjects)
            .HasForeignKey(ss => ss.StudentId);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.Students)
            .HasForeignKey(ss => ss.SubjectId);

        modelBuilder.Entity<StudentSubjectGrade>()
            .HasOne(ssg => ssg.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(ssg => ssg.StudentId);

        modelBuilder.Entity<StudentSubjectGrade>()
            .HasOne(ssg => ssg.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(ssg => ssg.SubjectId);

        // optimization
        modelBuilder.Entity<StudentSubjectGrade>()
            .Property(ssg => ssg.Timestamp)
            .HasColumnType("INTEGER")
            .HasConversion(
                dateTime => dateTime.Ticks,
                ticks => new DateTime(ticks));
    }
}
