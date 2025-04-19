
using StudentGradeTracker.Infra.DataContracts;

namespace StudentGradeTracker.Services;

public interface IServerApi
{
    public Task<IEnumerable<StudentDto>> GetStudentsAsync();
}
