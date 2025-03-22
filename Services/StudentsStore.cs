using StudentGradeTracker.Models;

namespace StudentGradeTracker.Services
{
    internal class StudentsStore : IStudentsStore
    {
        public StudentsStore()
        {
            Students = new()
             {
                new Student { Name = "Student 1", Grade = Grade.Excellent },
                new Student { Name = "Student 3", Grade = Grade.Poor },
                new Student { Name = "Student 4", Grade = Grade.Fail },
                new Student { Name = "Student 5", Grade = Grade.Good },
                new Student { Name = "Student 10", Grade = Grade.Fail },
            };
        }

        public List<Student> Students { get; private set; }

        public void AddStudent(Student newStudent)
        {
            Students.Add(newStudent);
        }

        public void RemoveStudent(Student studentToRemove)
        {
            if (Students.Contains(studentToRemove))
            {
                Students.Remove(studentToRemove);
            }
        }
    }
}
