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
        public bool SetReturnDate(Loan loan) // Ver como hacer para que solo actualice la fecha
        {
            dbSet.Attach(loan);
            var changedEntity = dbSet.Entry(loan).Property(l => l.ReturnDate).IsModified = true;
            return changedEntity;
        }
    }
}
