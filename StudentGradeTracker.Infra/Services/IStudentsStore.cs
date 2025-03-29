using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.Services
{
    public interface IStudentsStore
    {
        public List<Student> Students { get; }
        public Student AddStudent(Student newStudent);
        public Student RemoveStudent(Student studentToRemove);

    }
}
