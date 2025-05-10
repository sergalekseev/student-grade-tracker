using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;
using StudentGradeTrackerServer.Services;

namespace StudentGradeTrackerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsStore _studentsStore;
    private readonly IGradesStore _gradesStore;
    private readonly ISubjectsStore _subjectsStore;

    public StudentsController(IStudentsStore studentsStore,
        IGradesStore gradesStore, ISubjectsStore subjectsStore)
    {
        _studentsStore = studentsStore;
        _gradesStore = gradesStore;
        _subjectsStore = subjectsStore;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _studentsStore.GetStudentsAsync(CancellationToken.None); // TODO: add token

        return students
            .Select(DtoMapper.StudentToDto)
            .ToList();
    }

    [HttpGet("{idCard}")]
    public async Task<ActionResult<StudentDto>> GetStudent(string idCard)
    {
        var students = await _studentsStore.GetStudentsAsync(CancellationToken.None);

        return students
            .FirstOrDefault(x => x.IdCard.Equals(idCard))
            ?.ToDto();
    }

    [HttpDelete("{idCard}")]
    public async Task<ActionResult<StudentDto>> DeleteStudent(string idCard)
    {
        var removedStudent = await _studentsStore.RemoveStudentAsync(student => student.IdCard.Equals(idCard), CancellationToken.None);
        return removedStudent?.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(StudentCreateDto newStudent)
    {
        var createdStudent = await _studentsStore.AddStudentAsync(new Student()
        {
            IdCard = newStudent.IdCard,
            Name = newStudent.Name,
        }, CancellationToken.None);

        return createdStudent?.ToDto();
    }

    [HttpPut("{idCard}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(string idCard, 
        [FromBody]StudentUpdateDto studentToUpdate)
    {
        var student = await _studentsStore
            .GetStudentAsync(x => x.IdCard.Equals(idCard), CancellationToken.None);

        if (student is null)
        {
            return NotFound("Entity with specified id not found");
        }

        student.Name = studentToUpdate.Name;
        await _studentsStore.SaveChangesAsync(CancellationToken.None);

        return student.ToDto();
    }

    [HttpGet("{idCard}/grades")]
    public async Task<ActionResult> GetGrades(string idCard)
    {
        await Task.Delay(500);

        //var student = _studentsStore.Students
        //    .FirstOrDefault(x => x.IdCard.Equals(idCard));

        //if (student is null)
        //{
        //    return NotFound("Entity with specified id not found");
        //}

        //var subjectGrades = _gradesStore.Grades
        //    .Where(x => x.StudentId.Equals(student.Id))
        //    .Select(x => new SubjectGradeDto()
        //    {
        //        Subject = x.Subject.ToDto(),
        //        Grade = x.ToDto()
        //    })
        //    .ToList();

        //return Ok(new StudentGradesDto()
        //{
        //    Student = student.ToDto(),
        //    Grades = subjectGrades
        //});

        return Ok(new StudentGradesDto());
    }

    [HttpPost("{idCard}/grades")]
    public async Task<ActionResult> CreateGrade(string idCard,
        [FromBody]GradeCreateDto grade)
    {
        await Task.Delay(500);

        //var student = _studentsStore.Students
        //    .FirstOrDefault(x => x.IdCard.Equals(idCard));

        //if (student is null)
        //{
        //    return NotFound("Entity with specified id not found");
        //}

        //var subject = _subjectsStore.Subjects
        //    .FirstOrDefault(x => x.Id.Equals(grade.SubjectId));

        //if (subject is null)
        //{
        //    return NotFound("Entity with specified id not found");
        //}

        //// autoincrement
        //var lastId = _gradesStore.Grades.Max(x => x.Id);
        //var newGradeId = lastId + 1;

        //var resultGrade = _gradesStore.AddGrade(new StudentSubjectGrade()
        //{
        //    Id = newGradeId,
        //    StudentId = student.Id,
        //    SubjectId = subject.Id,
        //    Timestamp = DateTime.Now,
        //    Grade = grade.Grade,
        //});

        //return Ok(new
        //{
        //    Student = student.ToDto(),
        //    Subject = subject.ToDto(),
        //    Grade = resultGrade.ToDto()
        //});

        return Ok(new Object());
    }

    [HttpPut("{idCard}/subjects/{subjectId}")]
    public async Task<ActionResult> AssignSubject(string idCard, int subjectId)
    {
        await Task.Delay(500);

        //var student = _studentsStore.Students
        //    .FirstOrDefault(x => x.IdCard.Equals(idCard));

        //if (student is null)
        //{
        //    return NotFound("Entity with specified id not found");
        //}
        //var subject = _subjectsStore.Subjects
        //    .FirstOrDefault(x => x.Id.Equals(subjectId));

        //if (subject is null)
        //{
        //    return NotFound("Entity with specified id not found");
        //}

        //var studentSubject = _subjectsStore.StudentSubjects
        //    .FirstOrDefault(x =>
        //        x.StudentId.Equals(student.Id) &&
        //        x.SubjectId.Equals(subject.Id));

        //if (studentSubject is null)
        //{
        //    // autoincrement
        //    var lastId = _subjectsStore.StudentSubjects.Max(x => x.Id);
        //    var newId = lastId + 1;

        //    studentSubject = _subjectsStore.AddStudentSubject(new()
        //    {
        //        Id = newId,
        //        StudentId = student.Id,
        //        SubjectId = subject.Id
        //    });
        //}

        //return Ok(new
        //{
        //    Student = student.ToDto(),
        //    Subject = subject.ToDto()
        //});

        return Ok(new Object());
    }
}
