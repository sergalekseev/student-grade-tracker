using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public class SubjectsStore : ISubjectsStore
{
    public SubjectsStore(IStudentsStore studentsStore)
    {
        Subjects = new()
        {
            new() { Id = 1, Name = "Subject A1", Description = "Some subject A1 description" },
            new() { Id = 2, Name = "Subject A2" },
            new() { Id = 3, Name = "Subject B1" },
            new() { Id = 4, Name = "Subject B2", Description = "B2 subject description" },
            new() { Id = 5, Name = "Subject B3" },
        };

        StudentSubjects = new();
        int lastStudentSubjectId = 1;
        int maxSubjectId = Subjects.Max(i => i.Id);

        // blocking operation to initialize store
        var students = studentsStore.GetListAsync(CancellationToken.None).Result;

        foreach (var student in students)
        {
            var initialSubjectId = Math.Min(student.Id, maxSubjectId);

            for (var i = initialSubjectId; i <= maxSubjectId; i++)
            {
                var subject = Subjects.First(s => s.Id.Equals(i));
                var studentSubject = new StudentSubject()
                {
                    Id = lastStudentSubjectId++,
                    StudentId = student.Id,
                    SubjectId = i,

                    // refs
                    Student = student,
                    Subject = subject
                };

                StudentSubjects.Add(studentSubject);

                //refs
                subject.Students.Add(studentSubject);
                student.Subjects.Add(studentSubject);
            }
        }
    }

    private List<Subject> Subjects { get; set; }

    private List<StudentSubject> StudentSubjects { get; set; }

    public Task<StudentSubject> AddStudentSubjectAsync(StudentSubject newStudentSubject, CancellationToken cancellationToken)
    {
        StudentSubjects.Add(newStudentSubject);
        return Task.FromResult(newStudentSubject);
    }

    public Task<StudentSubject> RemoveStudentSubjectAsync(StudentSubject studentSubjectToRemove, CancellationToken cancellationToken)
    {
        StudentSubjects.Remove(studentSubjectToRemove);
        return Task.FromResult(studentSubjectToRemove);
    }

    public Task<Subject> AddAsync(Subject newEntity, CancellationToken cancellationToken)
    {
        newEntity.Id = Subjects.Max(x => x.Id) + 1;
        Subjects.Add(newEntity);

        return Task.FromResult(newEntity);
    }

    public Task<Subject?> GetAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
    {
        var subject = Subjects.FirstOrDefault(predicate.Compile());
        return Task.FromResult(subject);
    }

    public Task<IReadOnlyList<Subject>> GetListAsync(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyList<Subject>>(Subjects.AsReadOnly());

    public Task<IReadOnlyList<Subject>> GetListAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyList<Subject>>(Subjects.Where(predicate.Compile()).ToList().AsReadOnly());

    public Task<Subject> RemoveAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
    {
        var subject = Subjects.FirstOrDefault(predicate.Compile());

        if (subject is null)
        {
            throw new NullReferenceException(nameof(subject));
        }

        Subjects.Remove(subject);
        return Task.FromResult(subject);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
