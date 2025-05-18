using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public interface IGradesStore : IDataStore<StudentSubjectGrade>
{
    Task<List<StudentSubjectGrade>> GetGradesWithSubjectAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken);
}
