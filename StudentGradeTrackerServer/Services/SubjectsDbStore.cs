using Microsoft.EntityFrameworkCore;
using StudentGradeTrackerServer.Models;
using System.Linq.Expressions;

namespace StudentGradeTrackerServer.Services;

public class SubjectsDbStore : ISubjectsStore
{
    private readonly StudentsTrackerDbContext _dbContext;

    public SubjectsDbStore(StudentsTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subject> AddAsync(Subject newEntity, CancellationToken cancellationToken)
    {
        await _dbContext.Subjects.AddAsync(newEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newEntity;
    }

    public async Task<StudentSubject> AddStudentSubjectAsync(StudentSubject newStudentSubject, CancellationToken cancellationToken)
    {
        await _dbContext.StudentSubjects.AddAsync(newStudentSubject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newStudentSubject;
    }

    public Task<Subject?> GetAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
        => _dbContext.Subjects.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IReadOnlyList<Subject>> GetListAsync(CancellationToken cancellationToken)
        => await _dbContext.Subjects
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Subject>> GetListAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
        => await _dbContext.Subjects
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);

    public async Task<Subject> RemoveAsync(Expression<Func<Subject, bool>> predicate, CancellationToken cancellationToken)
    {
        var subject = await _dbContext.Subjects
            .FirstOrDefaultAsync(predicate);

        if (subject is null)
        {
            throw new NullReferenceException($"Entity {nameof(subject)} is not found");
        }

        _dbContext.Subjects.Remove(subject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return subject;
    }

    public async Task<StudentSubject> RemoveStudentSubjectAsync(Expression<Func<StudentSubject, bool>> predicate, CancellationToken cancellationToken)
    {
        var studentSubject = await _dbContext.StudentSubjects.FirstOrDefaultAsync(predicate);

        if (studentSubject is null)
        {
            throw new NullReferenceException(nameof(studentSubject));
        }

        _dbContext.StudentSubjects.Remove(studentSubject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return studentSubject;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
