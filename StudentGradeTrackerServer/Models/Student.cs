using StudentGradeTracker.Infra.Models;

namespace StudentGradeTrackerServer.Models;

public class Student
{
    public int Id { get; set; }
    public string IdCard { get; set; }
    public string Name { get; set; }
    public Grade Grade { get; set; }
}

