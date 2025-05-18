using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public interface ISubjectsStore : IDataStore<Subject>
{
    public Task<StudentSubject> AddStudentSubjectAsync(StudentSubject newStudentSubject, CancellationToken cancellationToken);
    public Task<StudentSubject> RemoveStudentSubjectAsync(Expression<Func<StudentSubject, bool>> predicate, CancellationToken cancellationToken);
}
