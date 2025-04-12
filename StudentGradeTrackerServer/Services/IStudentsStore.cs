using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services
{
    public interface IStudentsStore
    {
        public List<Student> Students { get; }
        public Student AddStudent(Student newStudent);
        public Student RemoveStudent(Student studentToRemove);

    }
}
