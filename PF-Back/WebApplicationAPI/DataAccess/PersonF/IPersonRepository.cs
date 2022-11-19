using Entities;

namespace WebApplicationAPI.DataAccess.PersonF
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        List<Loan> GetAllLoans();
        //bool Login(Person person);
    }
}
