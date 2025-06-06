﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
    private readonly IHubContext<NotificationHub, INotificationConnection> _notificationHub;

    public StudentsController(IStudentsStore studentsStore,
        IGradesStore gradesStore, ISubjectsStore subjectsStore,
        IHubContext<NotificationHub, INotificationConnection> notificationHub)
    {
        _studentsStore = studentsStore;
        _gradesStore = gradesStore;
        _subjectsStore = subjectsStore;
        _notificationHub = notificationHub;
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

            var result = createdStudent.ToDto();

            // notify all clients
            await _notificationHub.Clients.All
                .ReceiveMessage(new Message()
                {
                    Name = NotificationHubMessages.NewStudentUpdate,
                    Payload = result
                });
                //.ReceiveNewStudentUpdate(result);

            return result;
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
            .GetGradesWithSubjectAsync(x => x.StudentId.Equals(student.Id), cancellationToken);


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
        [FromBody]GradeCreateDto grade, CancellationToken cancellationToken)
    {
        var student = await _studentsStore.GetAsync(x => x.IdCard.Equals(idCard), cancellationToken);

        if (student is null)
        {
            return NotFound(student);
        }

        var subject = await _subjectsStore.GetAsync(x => x.Id.Equals(grade.SubjectId), cancellationToken);

        if (subject is null)
        {
            return NotFound(subject);
        }

        var resultGrade = await _gradesStore.AddAsync(new StudentSubjectGrade()
        {
            StudentId = student.Id,
            SubjectId = subject.Id,
            Timestamp = grade.Timestamp,
            Grade = grade.Grade,
        }, cancellationToken);

        return Ok(new SubjectGradeDto()
        {
            Subject = subject.ToDto(),
            Grade = resultGrade.ToDto()
        });
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
