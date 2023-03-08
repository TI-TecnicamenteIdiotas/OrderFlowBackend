﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using OrderFlow.Data.Context;
using OrderFlow.Business.Interfaces;
using OrderFlow.Business.Models;

namespace OrderFlow.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly OrderFlowContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(OrderFlowContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        /// <summary>
        /// Gets all entities of given type from database
        /// </summary>
        /// <param name="predicate">The function (where) to filter entities</param>
        /// <param name="track">Indicates wether the entity should be tracked or not.</param>
        /// <returns>A list of entities asynchronous</returns>
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, bool track = false)
        {
            return await (track ? DbSet.AsTracking() : DbSet.AsNoTracking()).ToListAsync();
        }

        /// <summary>
        /// Gets an entity of type by the given Guid
        /// </summary>
        /// <param name="id">The target entity's Id</param>
        /// <param name="track">Indicates wether the entity should be tracked or not.</param>
        /// <returns>The entity found or null if none</returns>
        public virtual async Task<TEntity> GetById(int id, bool track = false)
        {
            if(track)
                return await DbSet.AsTracking().FirstOrDefaultAsync(e => id.Equals(e.Id));
            else
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => id.Equals(e.Id));

        }

        /// <summary>
        /// Gets all entities of type as Queryable function for further filtering
        /// </summary>
        /// <returns>Queryable function</returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable<TEntity>();
        }

        /// <summary>
        /// Gets all entities of type without tracking
        /// </summary>
        /// <returns>A list containing all entities</returns>
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Adds a given entity to the context
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>The added entity</returns>
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChanges();
            return entity;
        }

        /// <summary>
        /// Updates the given entity to database
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>The same entity given</returns>
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
            return entity;
        }

        /// <summary>
        /// Removes given entity from database
        /// </summary>
        /// <param name="id">The Id of target entity</param>
        /// <returns>Task</returns>
        public virtual async Task Remove(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        /// <summary>
        /// Save all changes made to this context to database
        /// </summary>
        /// <returns>The number of changed entities</returns>
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}