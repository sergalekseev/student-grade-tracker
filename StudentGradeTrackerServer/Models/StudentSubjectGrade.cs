using StudentGradeTracker.Infra.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentGradeTrackerServer.Models;

public class StudentSubjectGrade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Required]
    public Grade Grade { get; set; }

    // refs
    public Student Student { get; set; }
    public Subject Subject { get; set; }
}
