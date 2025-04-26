using StudentGradeTracker.Infra.Models;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public class GradesStore : IGradesStore
{
    public GradesStore(ISubjectsStore subjectsStore, IStudentsStore studentsStore)
    {
        Grades = new();
        var rand = new Random();
        var maxGrade = Enum.GetValues(typeof(Grade)).Length - 1;
        var lastGradeId = 1;

        foreach (var studentSubject in subjectsStore.StudentSubjects)
        {
            var iterations = rand.Next(2, 5);
            var minGrade = rand.Next(1, maxGrade);

            for (var i = 1; i <= iterations; i++)
            {
                Grades.Add(new()
                {
                    Id = lastGradeId++,
                    StudentId = studentSubject.StudentId,
                    SubjectId = studentSubject.SubjectId,
                    Timestamp = DateTime.Now,
                    Grade = (Grade)rand.Next(minGrade, maxGrade),
                    
                    // refs
                    Student = studentSubject.Student,
                    Subject = studentSubject.Subject,
                });
            }
        }

        // calcuate grades
        foreach (var student in studentsStore.Students)
        {
            double averageGradeValue = Grades
                .Where(x => x.StudentId.Equals(student.Id))
                .Average(x => (int)x.Grade);

            student.Grade = GetAverageGrade(averageGradeValue);
        }
    }

    public List<StudentSubjectGrade> Grades { get; private set; }

    public StudentSubjectGrade AddGrade(StudentSubjectGrade studentSubjectGrade)
    {
        Grades.Add(studentSubjectGrade);
        return studentSubjectGrade;
    }

    public StudentSubjectGrade RemoveGrade(StudentSubjectGrade studentSubjectGrade)
    {
        Grades.Remove(studentSubjectGrade);
        return studentSubjectGrade;
    }

    private static Grade GetAverageGrade(double average) => average switch
    {
        < 1.0 => Grade.Fail,
        < 2.0 => Grade.Poor,
        < 3.0 => Grade.Good,
        _ => Grade.Excellent
    };
}
