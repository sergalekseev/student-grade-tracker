using StudentGradeTracker.Infra.Models;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services
{
    public class StudentsStore : IStudentsStore
    {
        public StudentsStore()
        {
            Students = new()
             {
                new Student { Id = 1, IdCard = "A0001", Name = "Student 1", Grade = Grade.Excellent },
                new Student { Id = 2, IdCard = "A0002", Name = "Student 4", Grade = Grade.Fail },
                new Student { Id = 3, IdCard = "B0001", Name = "Student 5", Grade = Grade.Good },
                new Student { Id = 4, IdCard = "C0001", Name = "Student 10", Grade = Grade.Fail },
                new Student { Id = 5, IdCard = "C0002", Name = "Student 3", Grade = Grade.Poor },
            };
        }

        public List<Student> Students { get; private set; }

        public Student AddStudent(Student newStudent)
        {
            Students.Add(newStudent);
            return newStudent;

        }

        public Student RemoveStudent(Student studentToRemove)
        {
            if (Students.Contains(studentToRemove))
            {
                Students.Remove(studentToRemove);
            }

            return studentToRemove;
        }
    }
}
