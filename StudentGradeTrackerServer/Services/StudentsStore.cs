using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services
{
    public class StudentsStore : IStudentsStore
    {
        public StudentsStore()
        {
            Students = new()
             {
                new Student { Id = 1, IdCard = "A0001", Name = "Student 1" },
                new Student { Id = 2, IdCard = "A0002", Name = "Student 4" },
                new Student { Id = 3, IdCard = "B0001", Name = "Student 5" },
                new Student { Id = 4, IdCard = "C0001", Name = "Student 10" },
                new Student { Id = 5, IdCard = "C0002", Name = "Student 3" },
            };
        }

        private List<Student> Students { get; set; }

        public Task<IReadOnlyList<Student>> GetListAsync(CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyList<Student>>(Students.AsReadOnly());

        public Task<IReadOnlyList<Student>> GetListAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyList<Student>>(Students.Where(predicate.Compile()).ToList().AsReadOnly());

        public Task<Student?> GetAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
        {
            var student = Students.FirstOrDefault(predicate.Compile());
            return Task.FromResult(student);
        }

        public Task<Student> AddAsync(Student newEntity, CancellationToken cancellationToken)
        {
            if (Students.Any(x => x.IdCard.Equals(newEntity.IdCard)))
            {
                throw new InvalidDataException($"Student with {nameof(Student.IdCard)} = {newEntity.IdCard} already exists");
            }

            newEntity.Id = Students.Max(x => x.Id) + 1;
            Students.Add(newEntity);

            return Task.FromResult(newEntity);

        }

        public Task<Student> RemoveAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
        {
            var student = Students.FirstOrDefault(predicate.Compile());

            if (student is null)
            {
                throw new NullReferenceException(nameof(student));
            }

            Students.Remove(student);
            return Task.FromResult(student);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
