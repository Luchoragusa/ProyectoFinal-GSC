using WebApplicationAPI.DataAccess.Thing;

namespace WebApplicationAPI.DataAccess
{
    public interface IUnitOfWork
    {
        PersonRepository PersonRepository { get; }
        int Complete();
    }
}
