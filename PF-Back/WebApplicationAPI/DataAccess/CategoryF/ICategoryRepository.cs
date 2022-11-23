using Entities;

namespace WebApplicationAPI.DataAccess.CategoryF
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category GetByDescrpition(string description);
    }
    
}
