using StudentGradeTracker.Infra.Models;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public class GradesStore : IGradesStore
{
    public GradesStore(ISubjectsStore subjectsStore)
    {
        Grades = new();
        var rand = new Random();
        var maxGrade = Enum.GetValues(typeof(Grade)).Length - 1;
        var lastGradeId = 1;

        foreach (var studentSubject in subjectsStore.StudentSubjects)
        {
            var iterations = rand.Next(2, 5);
            var minGrade = rand.Next(0, Math.Max(0, maxGrade - 2));

            for (var i = 1; i <= iterations; i++)
            {
                Grades.Add(new()
                {
                    Id = lastGradeId++,
                    StudentId = studentSubject.StudentId,
                    SubjectId = studentSubject.SubjectId,
                    Timestamp = DateTime.Now,
                    Grade = (Grade)rand.Next(minGrade, maxGrade)
                });
            }
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
}
