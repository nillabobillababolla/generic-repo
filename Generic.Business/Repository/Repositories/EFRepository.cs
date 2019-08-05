using System;
using System.Threading.Tasks;
using Generic.Business.Repository.Interfaces;
using Generic.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generic.Business.Repository.Repositories
{
    public abstract class EFRepository<TContext> : EFReadOnlyRepository<TContext>, IRepository where TContext : DbContext
    {
        protected EFRepository(TContext context) : base(context)
        { }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null) where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null) where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class, IEntity
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            DbSet<TEntity> dbSet = context.Set<TEntity>();

            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(DbUpdateException e)
        {
            var errorMessage = e.Message;

            var exceptionMessage = string.Concat(e.Message, " DbUpdateException: ", errorMessage);
            throw new DbUpdateException(exceptionMessage, e);
        }

    }

}