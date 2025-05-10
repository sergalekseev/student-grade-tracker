using Microsoft.EntityFrameworkCore;
using StudentGradeTrackerServer.Models;

namespace StudentGradeTrackerServer.Services;

public class DbStudentsStore : IStudentsStore
{
    private StudentsTrackerDbContext _dbContext;

    public DbStudentsStore(StudentsTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Student>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Student> GetStudentAsync(Func<Student, bool> predicate, CancellationToken cancellationToken)
    {
        var students = await _dbContext.Students // TODO: try to use db LINQ expression
           .ToListAsync(cancellationToken);

        return students.FirstOrDefault(predicate);
    }

    public async Task<Student> AddStudentAsync(Student newStudent, CancellationToken cancellationToken)
    {
        await _dbContext.Students.AddAsync(newStudent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newStudent;
    }

    public async Task<Student> RemoveStudentAsync(Student studentToRemove, CancellationToken cancellationToken)
    {
        _dbContext.Students.Remove(studentToRemove); // TODO: check why it's not async
        await _dbContext.SaveChangesAsync(cancellationToken); 

        return studentToRemove;
    }

    public async Task<Student> RemoveStudentAsync(Func<Student, bool> predicate, CancellationToken cancellationToken)
    {
        var students = await _dbContext.Students // TODO: try to use db LINQ expression
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var student = students.FirstOrDefault(predicate);

        // TODO: add check for null?

        _dbContext.Students.Remove(student); // TODO: check why it's not async
        await _dbContext.SaveChangesAsync(cancellationToken);

        return student;
     
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
