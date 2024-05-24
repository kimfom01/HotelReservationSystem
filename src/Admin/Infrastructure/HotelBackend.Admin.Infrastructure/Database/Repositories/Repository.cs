using System.Linq.Expressions;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(AdminDataContext adminDataContext)
    {
        DbSet = adminDataContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity entity)
    {
        var added = await DbSet.AddAsync(entity);

        return added.Entity;
    }

    public virtual async Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression)
    {
        return await DbSet.Where(expression).AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression);

        return entity;
    }
}