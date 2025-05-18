using Microsoft.EntityFrameworkCore;
using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public class GradesDbStore : IGradesStore
{
    private readonly StudentsTrackerDbContext _dbContext;

    public GradesDbStore(StudentsTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentSubjectGrade> AddAsync(StudentSubjectGrade newEntity, CancellationToken cancellationToken)
    {
        await _dbContext.Grades.AddAsync(newEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newEntity;
    }

    public Task<StudentSubjectGrade?> GetAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
        => _dbContext.Grades.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<List<StudentSubjectGrade>> GetGradesWithSubjectAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
        => await _dbContext.Grades
                .AsNoTracking()
                .Include(g => g.Subject)
                .Where(predicate)
                .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<StudentSubjectGrade>> GetListAsync(CancellationToken cancellationToken)
        => await _dbContext.Grades
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<StudentSubjectGrade>> GetListAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
        => await _dbContext.Grades
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);

    public async Task<StudentSubjectGrade> RemoveAsync(Expression<Func<StudentSubjectGrade, bool>> predicate, CancellationToken cancellationToken)
    {
        var grade = await _dbContext.Grades
            .FirstOrDefaultAsync(predicate);

        if (grade is null)
        {
            throw new NullReferenceException($"Entity {nameof(grade)} is not found");
        }

        _dbContext.Grades.Remove(grade);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return grade;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);
}
