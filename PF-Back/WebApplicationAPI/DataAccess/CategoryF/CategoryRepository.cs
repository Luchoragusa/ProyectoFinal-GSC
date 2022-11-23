using Entities;

namespace WebApplicationAPI.DataAccess.CategoryF
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebApplicationAPIContext context) : base(context)
        {
        }
        public Category GetByDescrpition(string description)
        {
            return context.Categories.FirstOrDefault(c => c.Description == description);
        }
    }
}
