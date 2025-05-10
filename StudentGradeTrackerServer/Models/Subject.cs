using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentGradeTrackerServer.Models;

public class Subject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }


    // refs
    public ICollection<StudentSubject> Students { get; set; } = new List<StudentSubject>();
    public ICollection<StudentSubjectGrade> Grades { get; set; } = new List<StudentSubjectGrade>();
}
