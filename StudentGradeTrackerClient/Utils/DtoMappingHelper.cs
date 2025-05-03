using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Utils;

internal class DtoMappingHelper
{
    public static List<SubjectGrades> SubjectGradesMapToViewModel(StudentGradesDto dto) => dto.Grades
        .Where(sg => sg?.Subject != null && sg?.Grade != null)
        .GroupBy(sg => sg.Subject.Name, StringComparer.OrdinalIgnoreCase)
        .Select(group => new SubjectGrades
        {
            SubjectName = group.Key,
            Grades = group
                .Select(sg => new GradeDto
                {
                    Timestamp = sg.Grade.Timestamp,
                    Grade = sg.Grade.Grade
                })
                .ToList()
        })
        .ToList();
}
