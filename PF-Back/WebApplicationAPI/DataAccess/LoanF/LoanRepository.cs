using Entities;

namespace WebApplicationAPI.DataAccess.LoanF
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(WebApplicationAPIContext context) : base(context)
        {
        }

        public List<Thing> GetAllThings()
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAllPerson()
        {
            throw new NotImplementedException();
        }
    }
}
