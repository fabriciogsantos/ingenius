using Ingenius.Data.Context;
using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ingenius.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity:Entity<TEntity>
    {

        protected IngeniusContext Db;
        protected DbSet<TEntity> DbSet;


        protected RepositoryBase(IngeniusContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();

        }

        public virtual int Add(TEntity obj)
        {
            DbSet.Add(obj);
           return Db.SaveChanges();
        }

        public virtual int Update(TEntity obj)
        {

            DbSet.Update(obj);
           return Db.SaveChanges();
        }

        public virtual IEnumerable<TEntity> GeetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }
        public IEnumerable<TEntity> GeetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }
        public virtual int Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));

            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

    }
}
