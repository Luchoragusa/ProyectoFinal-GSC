using Entities;

namespace WebApplicationAPI.DataAccess.Thing
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        List<Loan> GetAllLoans();
    }
}
