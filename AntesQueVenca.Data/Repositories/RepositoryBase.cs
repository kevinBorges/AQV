using AntesQueVenca.Data.Context;
using AntesQueVenca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntesQueVenca.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity>
        where TEntity : class
    {
        public AntesQueVencaContext Db = new AntesQueVencaContext(new DbContextOptions<AntesQueVencaContext>());

        public virtual void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
        }
        public virtual TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }
        public virtual void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
        }
        public virtual void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
        }
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            Db.SaveChanges();
        }
    }
}
