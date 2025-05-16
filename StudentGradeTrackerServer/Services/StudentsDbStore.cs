using Microsoft.EntityFrameworkCore;
using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public class StudentsDbStore : IStudentsStore
{
    private StudentsTrackerDbContext _dbContext;

    public StudentsDbStore(StudentsTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Student>> GetListAsync(CancellationToken cancellationToken)
        => await _dbContext.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Student>> GetListAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
        => await _dbContext.Students
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);

    public Task<Student?> GetAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
        => _dbContext.Students.FirstOrDefaultAsync(predicate);

    public async Task<Student> AddAsync(Student newEntity, CancellationToken cancellationToken)
    {
        await _dbContext.Students.AddAsync(newEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newEntity;
    }

    public async Task<Student> RemoveAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(predicate);

        if (student is null)
        {
            throw new NullReferenceException($"Entity {nameof(student)} is not found");
        }

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return student;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);




}
