using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;
using StudentGradeTrackerServer.Services;
using System.Threading;

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
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll(CancellationToken cancellationToken)
    {
        var students = await _studentsStore.GetListAsync(cancellationToken);

        return students
            .Select(DtoMapper.StudentToDto)
            .ToList();
    }

    [HttpGet("{idCard}")]
    public async Task<ActionResult<StudentDto>> GetStudent(string idCard, CancellationToken cancellationToken)
    {
        var student = await _studentsStore.GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

        if (student is null)
        {
            return NotFound(student);
        }

        return student.ToDto();
    }


    [HttpDelete("{idCard}")]
    public async Task<ActionResult<StudentDto>> DeleteStudent(string idCard, CancellationToken cancellationToken)
    {
        try
        {
            return await _studentsStore
                .RemoveAsync(x => x.IdCard.Equals(idCard), cancellationToken)
                .ToDtoAsync();
        }
        catch (NullReferenceException)
        {
            return NotFound(idCard);
        }
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(StudentCreateDto newStudent, CancellationToken cancellationToken)
    {
        try
        {
            var createdStudent = await _studentsStore.AddAsync(new Student()
            {
                IdCard = newStudent.IdCard,
                Name = newStudent.Name,
            }, cancellationToken);

            return createdStudent.ToDto();
        }
        catch (Exception ex) when (ex is DbUpdateException or InvalidDataException)
        {
            return UnprocessableEntity(newStudent);
        }
    }

    [HttpPut("{idCard}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(string idCard, 
        [FromBody]StudentUpdateDto studentToUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var student = await _studentsStore
                .GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

            if (student is null)
            {
                return NotFound(idCard);
            }

            student.Name = studentToUpdate.Name;
            await _studentsStore.SaveChangesAsync(cancellationToken);

            return student.ToDto();
        }
        catch (DbUpdateException)
        {
            return UnprocessableEntity(studentToUpdate);
        }
    }

    [HttpGet("{idCard}/subjects")]
    public async Task<ActionResult<StudentSubjectsDto>> GetSubjects(string idCard, CancellationToken cancellationToken)
    {
        var student = await _studentsStore.GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

        if (student is null)
        {
            return NotFound(student);
        }

        return Ok(new StudentSubjectsDto()
        {
            Student = student.ToDto(),
            Subjects = student.Subjects.Select(x => x.Subject.ToDto()).ToList()
        });
    }

    [HttpGet("{idCard}/grades")]
    public async Task<ActionResult> GetGrades(string idCard, CancellationToken cancellationToken)
    {
        var student = await _studentsStore
            .GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

        if (student is null)
        {
            return NotFound(student);
        }

        var subjectGrades = await _gradesStore
            .GetListAsync(x => x.StudentId.Equals(student.Id), cancellationToken);


        return Ok(new StudentGradesDto()
        {
            Student = student.ToDto(),
            Grades = subjectGrades.Select(x => new SubjectGradeDto()
            {
                Subject = x.Subject.ToDto(),
                Grade = x.ToDto()
            }).ToList()
        });
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
    public async Task<ActionResult> AssignSubject(string idCard, int subjectId, CancellationToken cancellationToken)
    {
        var student = await _studentsStore
            .GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

        if (student is null)
        {
            return NotFound(student);
        }

        var subject = _subjectsStore
            .GetAsync(x => x.Id.Equals(subjectId), cancellationToken);

        if (subject is null)
        {
            return NotFound(subject);
        }

        var studentSubject = await _subjectsStore.AddStudentSubjectAsync(new()
        {
            StudentId = student.Id,
            SubjectId = subjectId
        }, cancellationToken);

        return Ok(new StudentSubjectDto()
        {
            Student = studentSubject.Student.ToDto(),
            Subject = studentSubject.Subject.ToDto(),
        });
    }
}
