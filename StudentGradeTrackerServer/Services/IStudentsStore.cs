using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services
{
    public interface IStudentsStore
    {
        public Task<IReadOnlyList<Student>> GetStudentsAsync(CancellationToken cancellationToken);
        public Task<Student> GetStudentAsync(Func<Student, bool> predicate, CancellationToken cancellationToken);
        public Task<Student> AddStudentAsync(Student newStudent, CancellationToken cancellationToken);
        public Task<Student> RemoveStudentAsync(Student studentToRemove, CancellationToken cancellationToken);
        public Task<Student> RemoveStudentAsync(Func<Student, bool> predicate, CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
