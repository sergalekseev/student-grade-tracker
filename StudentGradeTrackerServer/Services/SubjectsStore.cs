using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public class SubjectsStore : ISubjectsStore
{
    public SubjectsStore(IStudentsStore studentsStore)
    {
        Subjects = new()
        {
            new() { Id = 1, Name = "Subject A1", Description = "Some subject A1 description" },
            new() { Id = 2, Name = "Subject A2" },
            new() { Id = 3, Name = "Subject B1" },
            new() { Id = 4, Name = "Subject B2", Description = "B2 subject description" },
            new() { Id = 5, Name = "Subject B3" },
        };

        StudentSubjects = new();
        int lastStudentSubjectId = 1;
        int maxSubjectId = Subjects.Max(i => i.Id);

        foreach (var student in studentsStore.Students)
        {
            var initialSubjectId = Math.Min(student.Id, maxSubjectId);

            for (var i = initialSubjectId; i <= maxSubjectId; i++)
            {
                StudentSubjects.Add(new()
                {
                    Id = lastStudentSubjectId++,
                    StudentId = student.Id,
                    SubjectId = i,

                    // refs
                    Student = student,
                    Subject = Subjects.First(s => s.Id.Equals(i))
                });
            }
        }
    }

    public List<Subject> Subjects { get; private set; }

    public List<StudentSubject> StudentSubjects { get; private set; }

    public StudentSubject AddStudentSubject(StudentSubject newStudentSubject)
    {
        StudentSubjects.Add(newStudentSubject);
        return newStudentSubject;
    }

    public Subject AddSubject(Subject newSubject)
    {
        Subjects.Add(newSubject);
        return newSubject;
    }

    public StudentSubject RemoveStudentSubject(StudentSubject studentSubjectToRemove)
    {
        StudentSubjects.Remove(studentSubjectToRemove);
        return studentSubjectToRemove;
    }

    public Subject RemoveSubject(Subject subjectToRemove)
    {
        Subjects.Remove(subjectToRemove);
        return subjectToRemove;
    }
}
