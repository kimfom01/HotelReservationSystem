﻿using System.Linq.Expressions;
using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class ReservationsBaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected ReservationsBaseRepository(ReservationDataContext reservationDataContext)
    {
        DbSet = reservationDataContext.Set<TEntity>();
    }

    public async Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken)
    {
        var added = await DbSet.AddAsync(entity, cancellationToken);

        return added.Entity;
    }

    public Task AddMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        return DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await DbSet.Where(expression).AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        var entity = await DbSet.FirstOrDefaultAsync(expression, cancellationToken);

        return entity;
    }
}