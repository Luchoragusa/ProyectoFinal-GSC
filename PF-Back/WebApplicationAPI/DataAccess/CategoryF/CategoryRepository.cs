using Entities;

namespace WebApplicationAPI.DataAccess.CategoryF
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebApplicationAPIContext context) : base(context)
        {
        }
    }
}
