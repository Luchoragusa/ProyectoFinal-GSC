using WebApplicationAPI.DataAccess.Thing;

namespace WebApplicationAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebApplicationAPIContext _context;
        public UnitOfWork(WebApplicationAPIContext context)
        {
            _context = context;
            PersonRepository = new PersonRepository(_context);
        }
        
        public PersonRepository PersonRepository { get; } 
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
