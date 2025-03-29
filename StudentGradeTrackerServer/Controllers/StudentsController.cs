using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Infra.Models;
using StudentGradeTracker.Infra.Services;

namespace StudentGradeTrackerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsStore _studentsStore;

    public StudentsController(IStudentsStore studentsStore)
    {
        _studentsStore = studentsStore;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentResponse>>> GetAll()
    {
        await Task.Delay(500);
        return _studentsStore.Students.Select(x => new GetStudentResponse()
        {
            Name = x.Name,
            Grade = x.Grade
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentResponse>> GetStudent(int id)
    {
        await Task.Delay(500);
        var student = _studentsStore.Students.FirstOrDefault(x => x.Id.Equals(id));
        GetStudentResponse response = null;

        if (student != null)
        {
            response = new GetStudentResponse()
            {
                Name = student.Name,
                Grade = student.Grade
            };
        }

        return response;
    }

    [HttpPost]
    public async Task<ActionResult<GetStudentResponse>> AddStudent(AddStudentRequest newStudent)
    {
        await Task.Delay(500);

        // autoincrement
        var lastId = _studentsStore.Students.Max(x => x.Id);
        var newStudentId = lastId + 1;

        var resultStudent = _studentsStore.AddStudent(new Student()
        {
            Id = newStudentId,
            Name = newStudent.Name,
            Grade = newStudent.Grade
        });

        return new GetStudentResponse()
        {
            Name = resultStudent.Name,
            Grade = resultStudent.Grade
        };
    }
}
