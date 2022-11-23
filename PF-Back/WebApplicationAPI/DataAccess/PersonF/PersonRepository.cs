using Entities;
using Microsoft.IdentityModel.Tokens;
using WebApplicationAPI.Dto;

namespace WebApplicationAPI.DataAccess.PersonF
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(WebApplicationAPIContext context) : base(context)
        {
        }

        public Person GetByEmail(Person person)
        {
            return context.Person.SingleOrDefault(x => x.Email == person.Email);
        }
        public new List<PersonDTO> GetAll()
        {
            return context.Person.Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Role = x.Role
            }).ToList();
        }
        public PersonDTO GetByIdDTO(int id)
        {
            return context.Person.Where(x => x.Id == id).Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Role = x.Role
            }).SingleOrDefault();
        }
        public Person Update(PersonDTO person)
        {
            Person p = new Person
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone
            };

            if (!person.Name.IsNullOrEmpty())
                context.Entry(p).Property("Name").IsModified = true;
            if (!person.Email.IsNullOrEmpty())
                context.Entry(p).Property("Email").IsModified = true;
            if (!person.Phone.IsNullOrEmpty())
                context.Entry(p).Property("Phone").IsModified = true;

            return p;
        }
    }
}
