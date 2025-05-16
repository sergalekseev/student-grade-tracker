using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public interface IGradesStore : IDataStore<StudentSubjectGrade>
{
}
