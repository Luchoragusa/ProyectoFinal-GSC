using Entities;

namespace WebApplicationAPI.DataAccess.LoanF
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        List<Loan> GetLoansForPerson(Person person);
        bool SetReturnDate(Loan loan);
    }
}
