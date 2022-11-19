using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.DataAccess.LoanF;
using WebApplicationAPI.DataAccess.PersonF;

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
        }
        
        public PersonRepository PersonRepository { get; }
        public LoanRepository LoanRepository { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
