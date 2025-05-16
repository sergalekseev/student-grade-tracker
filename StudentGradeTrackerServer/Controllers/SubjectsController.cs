using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeTracker.Infra.DataContracts;
using StudentGradeTrackerServer.Models;
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
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll(CancellationToken cancellationToken)
        {
            var subjects = await _subjectsStore.GetListAsync(cancellationToken);

            return subjects
                .Select(DtoMapper.SubjectToDto)
                .ToList();
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(
            [FromBody]SubjectDto newSubject, CancellationToken cancellationToken)
        {
            try
            {
                var createdStudent = await _subjectsStore.AddAsync(new Subject()
                {
                    Name = newSubject.Name,
                    Description = newSubject.Description,
                }, cancellationToken);

                return createdStudent.ToDto();
            }
            catch (Exception ex) when (ex is DbUpdateException or InvalidDataException)
            {
                return UnprocessableEntity(newSubject);
            }
        }
    }
}
