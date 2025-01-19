using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Persistence.Repositories;
public abstract class BaseRepository<TModel, TKey>(PMDbContext context) : IRepository<TModel, TKey> where TModel : class
{
    protected readonly PMDbContext context = context;

    public virtual IUnitOfWork UnitOfWork
    {
        get
        {
            return context;
        }
    }
    public virtual TModel Add(TModel entity)
    {
        return context.Set<TModel>().Add(entity).Entity;
    }

    public virtual async Task<TModel> GetById(TKey id, CancellationToken cancellationToken)
    {
        return await context.Set<TModel>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual void Update(TModel entity)
    {
        context.Set<TModel>().Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TModel entity)
    {
        context.Set<TModel>().Remove(entity);
    }
}
