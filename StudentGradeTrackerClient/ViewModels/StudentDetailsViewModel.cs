using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTracker.Models;
using StudentGradeTracker.Services;
using StudentGradeTracker.Utils;

namespace StudentGradeTracker.ViewModels;

public class StudentDetailsViewModel : BaseViewModel
{
    private readonly IServerApi _serverApi;

    public StudentDetailsViewModel(IServerApi serverApi)
    {
        _serverApi = serverApi;
    }

    public StudentDto Student { get; set; }
    public List<SubjectGrades> SubjectGrades { get; set; }

    public async Task InitializeAsync()
    {
        var grades = await _serverApi.GetGradesAsync(Student.IdCard);
        SubjectGrades = DtoMappingHelper.SubjectGradesMapToViewModel(grades);
        InvokeOnPropertyChangedEvent(nameof(SubjectGrades));
    }
}
