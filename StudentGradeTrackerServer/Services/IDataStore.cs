using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace StudentGradeTrackerServer.Services;

public interface IDataStore<TEntity> where TEntity : class
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<IReadOnlyList<TEntity>> GetListAsync(CancellationToken cancellationToken);
    public Task<IReadOnlyList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    /// <exception cref="DbUpdateException" />
    /// <exception cref="InvalidDataException" />
    public Task<TEntity> AddAsync(TEntity newEntity, CancellationToken cancellationToken);
    public Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}

