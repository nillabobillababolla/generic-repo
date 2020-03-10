using System;
using System.Threading.Tasks;
using Generic.Business.Repository.Interfaces;
using Generic.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generic.Business.Repository.Repositories
{
    public abstract class EfRepository<TContext> : EfReadOnlyRepository<TContext>, IRepository where TContext : DbContext
    {
        protected EfRepository(TContext context) : base(context)
        { }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;

            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            var entity = Context.Set<TEntity>().Find(id);

            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context.Set<TEntity>();

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ThrowEnhancedValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(DbUpdateException ex)
        {
            var errorMessage = ex.Message;
            var exceptionMessage = string.Concat(ex.Message, " DbUpdateException: ", errorMessage);

            throw new DbUpdateException(exceptionMessage, ex);
        }
    }
}
