
using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTracker.Services;

public interface IServerApi
{
    public Task<IEnumerable<StudentDto>> GetStudentsAsync();
    public Task<StudentDto> CreateStudentAsync(StudentCreateDto newStudent);
    public Task<StudentDto> RemoveStudentAsync(string idCard);
}
