using StudentGradeTracker.Infra.Models;

namespace StudentGradeTracker.Infra.Services
{
    public class StudentsStore : IStudentsStore
    {
        public StudentsStore()
        {
            Students = new()
             {
                new Student { Id = 1, Name = "Student 1", Grade = Grade.Excellent },
                new Student { Id = 2, Name = "Student 4", Grade = Grade.Fail },
                new Student { Id = 3, Name = "Student 5", Grade = Grade.Good },
                new Student { Id = 4, Name = "Student 10", Grade = Grade.Fail },
                new Student { Id = 5, Name = "Student 3", Grade = Grade.Poor },
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
