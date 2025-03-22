using StudentGradeTracker.Models;

namespace StudentGradeTracker.Services
{
    public interface IStudentsStore
    {
        public List<Student> Students { get; }
        public void AddStudent(Student newStudent);
        public void RemoveStudent(Student studentToRemove);

    }
}
