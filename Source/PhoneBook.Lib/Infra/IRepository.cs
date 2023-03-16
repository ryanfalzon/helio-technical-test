using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace PhoneBook.Lib.Infra;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<TEntity?> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task InsertAsync(TEntity entity);

    bool Delete(object id);

    bool Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);
}