using Entities;
using WebApplicationAPI.Dto;

namespace WebApplicationAPI.DataAccess.PersonF
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Person GetByEmail(Person person);
        new List<PersonDTO> GetAll();
        PersonDTO GetByIdDTO(int id);
        Person Update(PersonDTO person);
    }
}
