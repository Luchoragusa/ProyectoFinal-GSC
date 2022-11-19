using Entities;

namespace WebApplicationAPI.DataAccess.LoanF
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        List<Thing> GetAllThings();
        List<Person> GetAllPerson();
        bool SetReturnDate(Loan loan);
    }
}
