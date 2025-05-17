using StudentGradeTracker.Infra.Models;
using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public class GradesStore : IGradesStore
{
    public GradesStore(IStudentsStore studentsStore, ISubjectsStore subjectsStore)
    {
        Grades = new();
        var rand = new Random();
        var maxGrade = Enum.GetValues(typeof(Grade)).Length - 1;
        var lastGradeId = 1;

        // blocking operation to initialize store
        var students = studentsStore.GetListAsync(CancellationToken.None).Result;

        foreach (var student in students)
        {
            foreach (var studentSubject in student.Subjects)
            {
                var iterations = rand.Next(2, 5);
                var minGrade = rand.Next(1, maxGrade);

                for (var i = 1; i <= iterations; i++)
                {
                    var grade = new StudentSubjectGrade()
                    {
                        Id = lastGradeId++,
                        StudentId = studentSubject.StudentId,
                        SubjectId = studentSubject.SubjectId,
                        Timestamp = DateTime.Now,
                        Grade = (Grade)rand.Next(minGrade, maxGrade),

                        // refs
                        Student = studentSubject.Student,
                        Subject = studentSubject.Subject,
                    };

                    Grades.Add(grade);

                    //refs
                    studentSubject.Student.Grades.Add(grade);
                    studentSubject.Subject.Grades.Add(grade);
                }
            }
        }
    }

    private List<StudentSubjectGrade> Grades { get; set; }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task<IReadOnlyList<StudentSubjectGrade>> GetListAsync(CancellationToken cancellationToken) =>
        Task.FromResult<IReadOnlyList<StudentSubjectGrade>>(Grades.AsReadOnly());

    public Task<IReadOnlyList<StudentSubjectGrade>> GetListAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken) =>
        Task.FromResult<IReadOnlyList<StudentSubjectGrade>>(Grades.Where(predicate.Compile()).ToList().AsReadOnly());

    public Task<StudentSubjectGrade?> GetAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
    {
        var grade = Grades.FirstOrDefault(predicate.Compile());
        return Task.FromResult(grade);
    }

    public Task<StudentSubjectGrade> AddAsync(StudentSubjectGrade newEntity, CancellationToken cancellationToken)
    {
        newEntity.Id = Grades.Max(x => x.Id) + 1;
        Grades.Add(newEntity);

        return Task.FromResult(newEntity);
    }

    public Task<StudentSubjectGrade> RemoveAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
    {
        var grade = Grades.FirstOrDefault(predicate.Compile());

        if (grade is null)
        {
            throw new NullReferenceException(nameof(grade));
        }

        Grades.Remove(grade);
        return Task.FromResult(grade);
    }
}
