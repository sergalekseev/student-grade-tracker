using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public interface ISubjectsStore : IDataStore<Subject>
{
    public Task<StudentSubject> AddStudentSubjectAsync(StudentSubject newStudentSubject, CancellationToken cancellationToken);
    public Task<StudentSubject> RemoveStudentSubjectAsync(StudentSubject studentSubjectToRemove, CancellationToken cancellationToken);
}
