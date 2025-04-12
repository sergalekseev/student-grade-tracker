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

    public StudentsController(IStudentsStore studentsStore)
    {
        _studentsStore = studentsStore;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        await Task.Delay(500);

        return _studentsStore.Students
            .Select(DtoMapper.StudentToDto)
            .ToList();
    }

    [HttpGet("{idCard}")]
    public async Task<ActionResult<StudentDto>> GetStudent(string idCard)
    {
        await Task.Delay(500);

        return _studentsStore.Students
            .FirstOrDefault(x => x.IdCard.Equals(idCard))
            ?.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(StudentCreateDto newStudent)
    {
        await Task.Delay(500);

        // autoincrement
        var lastId = _studentsStore.Students.Max(x => x.Id);
        var newStudentId = lastId + 1;

        return _studentsStore.AddStudent(new Student()
        {
            Id = newStudentId,
            IdCard = newStudent.IdCard,
            Name = newStudent.Name,
        }).ToDto();
    }

    [HttpPut("{idCard}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(string idCard, 
        [FromBody]StudentUpdateDto studentToUpdate)
    {
        await Task.Delay(500);

        var student = _studentsStore.Students
            .FirstOrDefault(x => x.IdCard.Equals(idCard));

        if (student is null)
        {
            return NotFound("Entity with specified id not found");
        }

        student.Name = studentToUpdate.Name;

        return student.ToDto();
    }
}
