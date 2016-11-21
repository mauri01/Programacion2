using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace SGITO_OBRAS.Entity.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected SGITO_OBRASContext Context = null;

        public Repository(SGITO_OBRASContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return (DbSet<TEntity>)Context.Set<TEntity>();
            }
        }

        public DbSet<TEntity> GetDbSet()
        {
            return (DbSet<TEntity>)Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All()
        {
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                return DbSet.Where("habilitado").AsQueryable<TEntity>();
            }
            else
            {
                return DbSet.AsQueryable();
            }
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                return DbSet.Where(predicate).Where("habilitado").AsQueryable<TEntity>();
            }
            else
            {
                return DbSet.Where(predicate).AsQueryable<TEntity>();
            }
        }

        public virtual IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                var _resetSet = filter != null ? DbSet.Where(filter).Where("habilitado") : DbSet;
                _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.AsEnumerable().Skip(skipCount).Take(size).AsQueryable();
                total = _resetSet.Where("habilitado").Count();
                return _resetSet;
            }
            else
            {
                //int skipCount = index * size;
                var _resetSet = filter != null ? DbSet.Where(filter) : DbSet;
                _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.AsEnumerable().Skip(skipCount).Take(size).AsQueryable();
                total = _resetSet.Count();
                return _resetSet;
            }
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                return DbSet.Where("habilitado").Count(predicate) > 0;
            }
            else
            {
                return DbSet.Count(predicate) > 0;
            }
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                return DbSet.Where("habilitado").FirstOrDefault(predicate);
            }
            else
            {
                return DbSet.FirstOrDefault(predicate);
            }
        }

        public virtual TEntity Create(TEntity TEntity)
        {
            try
            {
                if (UtilRepo.HasMember(TEntity, "habilitado"))
                {
                    Context.Entry(TEntity).Member("habilitado").CurrentValue = true;
                }
                var newEntry = DbSet.Add(TEntity);
                return newEntry;
            }
            catch
            {
                return null;
            }


        }

        public virtual void Delete(TEntity TEntity, bool? forzarRetiro = null)
        {
            if (Context.Entry(TEntity).State == EntityState.Detached)
            {
                DbSet.Attach(TEntity);
            }
            if (forzarRetiro != null)
            {
                if (forzarRetiro.Value)
                {
                    DbSet.Remove(TEntity);
                }
            }
            else
            {
                if (UtilRepo.HasMember(TEntity, "habilitado"))
                {
                    this.Disabled(TEntity);
                }
                else
                {
                    DbSet.Remove(TEntity);
                }
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }
        }

        public virtual void Update(TEntity TEntity)
        {
            DbSet.Attach(TEntity);
            Context.Entry(TEntity).State = EntityState.Modified;
        }

        //Esta función desabilita un registro dado
        public virtual void Disabled(TEntity TEntity)
        {
            DbSet.Attach(TEntity);
            Context.Entry(TEntity).Member("habilitado").CurrentValue = false;
            Context.Entry(TEntity).State = EntityState.Modified;
        }

        public virtual int Count
        {
            get
            {
                var name = typeof(TEntity);
                if (UtilRepo.GetMember(name, "habilitado"))
                {
                    if (DbSet.Where("habilitado").Count() > 0)
                    {
                        return DbSet.Count();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (DbSet.Count() > 0)
                    {
                        return DbSet.Count();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public virtual int CountWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            if (UtilRepo.GetMember(name, "habilitado"))
            {
                return DbSet.Where("habilitado").Count(predicate);
            }
            else
            {
                return DbSet.Count(predicate);
            }
        }

        public virtual TEntity Last()
        {
            return All().ToList().Last();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

    public static class UtilRepo
    {

        public static bool HasMember(this object objectToCheck, string memberName)
        {
            var type = objectToCheck.GetType();
            var a = type.GetMember(memberName);
            var habilitado = false;
            if (a.Count() > 0)
            {
                habilitado = true;
            }
            return habilitado;
        }

        public static bool GetMember(Type TEntity, string memberName)
        {
            var a = TEntity.GetMember(memberName);
            var habilitado = false;
            if (a.Count() > 0)
            {
                habilitado = true;
            }
            return habilitado;
        }

    }
}
