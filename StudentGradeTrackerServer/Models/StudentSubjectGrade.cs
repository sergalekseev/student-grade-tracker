using StudentGradeTracker.Infra.Models;

namespace StudentGradeTrackerServer.Models;

public class StudentSubjectGrade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public DateTime Timestamp { get; set; }
    public Grade Grade { get; set; }
}
