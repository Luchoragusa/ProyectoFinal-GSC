using WebApplicationAPI.DataAccess.LoanF;
using WebApplicationAPI.DataAccess.PersonF;
using WebApplicationAPI.DataAccess.CategoryF;

namespace WebApplicationAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebApplicationAPIContext _context;
        public UnitOfWork(WebApplicationAPIContext context)
        {
            _context = context;
            PersonRepository = new PersonRepository(_context);
            LoanRepository = new LoanRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }
        
        public PersonRepository PersonRepository { get; }
        public LoanRepository LoanRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
