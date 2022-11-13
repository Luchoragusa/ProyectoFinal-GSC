using Entities;

namespace WebApplicationAPI.DataAccess.Thing
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(WebApplicationAPIContext context) : base(context)
        {
        }

        public List<Loan> GetAllLoans()
        {
            throw new NotImplementedException();
        }
    }
}
