using Entities;

namespace WebApplicationAPI.DataAccess.PersonF
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(WebApplicationAPIContext context) : base(context)
        {
        }

        //bool Login(Person person)
        //{
        //    return true;
        //}
    }
}
