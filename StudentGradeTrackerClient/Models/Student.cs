namespace StudentGradeTracker.Models
{
    public enum Grade
    {
        Fail,
        Poor,
        Good,
        Excellent
    }

    public class Student
    {
        public string Name { get; set; }
        public Grade Grade { get; set; }
    }
}
