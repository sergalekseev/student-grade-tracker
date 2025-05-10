using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradeTrackerServer.Models;

[Index(nameof(IdCard), IsUnique = true)]
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(5, MinimumLength = 5)] // constraint doesn't work for some reason
    public string IdCard { get; set; }

    [Required]
    public string Name { get; set; }

    //refs
    public ICollection<StudentSubject> Subjects { get; set; } = new List<StudentSubject>();
    public ICollection<StudentSubjectGrade> Grades { get; set; } = new List<StudentSubjectGrade>();
}


