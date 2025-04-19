using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Services;

namespace StudentGradeTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsStore _subjectsStore;

        public SubjectsController(ISubjectsStore subjectsStore)
        {
            _subjectsStore = subjectsStore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll()
        {
            await Task.Delay(500);

            return _subjectsStore.Subjects
                .Select(DtoMapper.SubjectToDto)
                .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(
            [FromBody]SubjectDto newSubject)
        {
            await Task.Delay(500);

            // autoincrement
            var lastId = _subjectsStore.Subjects.Max(x => x.Id);
            var newSubjectId = lastId + 1;

            return _subjectsStore.AddSubject(new Models.Subject()
            {
                Id = newSubjectId,
                Name = newSubject.Name,
                Description = newSubject.Description,
            }).ToDto();
        }
    }
}
