using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace PhoneBook.Lib.Infra;

public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected GenericRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet   = dbContext.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = DbSet;
        
        if (include != null)
        {
            query = include(query);
        }
        
        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual bool Delete(object id)
    {
        var entityToDelete = DbSet.Find(id);

        if (entityToDelete != null)
        {
            Delete(entityToDelete);
            return true;
        }

        return false;
    }

    public virtual bool Delete(TEntity entityToDelete)
    {
        if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }
        DbSet.Remove(entityToDelete);

        return true;
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        DbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }
}