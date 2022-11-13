using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplicationAPI.DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : EntityBase

    {
        protected WebApplicationAPIContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(WebApplicationAPIContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public TEntity GetById(int id)
        {
            return dbSet.SingleOrDefault(x => x.Id == id);
        }
        public TEntity Insert(TEntity entity)
        {
            var savedEntity = dbSet.Add(entity);
            return savedEntity.Entity;
        }
        public TEntity Update(TEntity entity)
        {
            var changedEntity = dbSet.Update(entity);
            return changedEntity.Entity;
        }
        public bool Delete(int id)
        {
            var element = dbSet.Find(id);
            if (element == null)
                return false;
            dbSet.Remove(element);
            return true;
        }
    }
}