namespace StudentGradeTracker.Infra.Models;

public enum Grade
{
    Fail,
    Poor,
    Good,
    Excellent
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Grade Grade { get; set; }
}

