using WebApplicationAPI.DataAccess.PersonF;
using WebApplicationAPI.DataAccess.LoanF;

namespace WebApplicationAPI.DataAccess
{
    public interface IUnitOfWork
    {
        PersonRepository PersonRepository { get; }
        LoanRepository LoanRepository { get; }
        int Complete();
    }
}
