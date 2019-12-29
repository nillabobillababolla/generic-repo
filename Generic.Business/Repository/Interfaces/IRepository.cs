using System.Threading.Tasks;
using Generic.Entity.Interfaces;

namespace Generic.Business.Repository.Interfaces
{
    public interface IRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
       where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Save();

        Task SaveAsync();
    }
}
