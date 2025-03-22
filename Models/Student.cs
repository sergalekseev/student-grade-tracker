namespace StudentGradeTracker.Models
{
    enum Grade
    {
        Fail,
        Poor,
        Good,
        Excellent
    }

    internal class Student
    {
        public string Name { get; set; }
        public Grade Grade { get; set; }
    }
}
