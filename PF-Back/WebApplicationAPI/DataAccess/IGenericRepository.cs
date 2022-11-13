using Entities;

namespace WebApplicationAPI.DataAccess
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
    }
}
