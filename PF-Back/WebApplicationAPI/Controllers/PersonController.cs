using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DataAccess;
using Entities;
using WebApplicationAPI.Helpers;
using WebApplicationAPI.Dto;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public PersonController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet] // GetAll api/person
        public IActionResult GetAll()
        {
            List<PersonDTO> persons = uow.PersonRepository.GetAll();
            if (persons == null)
                return NotFound("No persons found");
            return Ok(uow.PersonRepository.GetAll());
        }

        [HttpGet("{id}")] // GetOne api/person/{id}
        public IActionResult GetOne(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            PersonDTO person = uow.PersonRepository.GetByIdDTO(id);
            if (person == null)
                return NotFound();
            
            return Ok(person);
        }

        [HttpPost] // POST api/person
        public IActionResult Register([FromBody] Person person)
        {
            //Request validations
            if (person is null)
                return BadRequest("Body is empty");
            
            if (string.IsNullOrWhiteSpace(person.Name))
                return BadRequest("Name is mandatory");
            if (string.IsNullOrWhiteSpace(person.Email))
                return BadRequest("Email is mandatory");
            if (string.IsNullOrWhiteSpace(person.Phone))
                return BadRequest("Phone is mandatory");
            if (string.IsNullOrWhiteSpace(person.Password))
                return BadRequest("Password is mandatory");

            person.Role = 2; // Default Role user
            person.Password = Hash.HashPassword(person.Password);// Hash password
            
            Person p_created = uow.PersonRepository.Insert(person);

            if (p_created == null)
                return BadRequest("Error creating person");

            uow.Complete();
            
            return Created($"/person/{p_created.Id}", p_created); 
        }

        [HttpPut("{id}")] // PUT api/person/{id}
        public IActionResult Update(int id, [FromBody] Person person)
        {
            //Request validations
            if (person is null)
                return BadRequest("Body is empty");
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            // Body validations
            bool band = false;
            if (!string.IsNullOrWhiteSpace(person.Name))
                band = true;
            if (!string.IsNullOrWhiteSpace(person.Email))
                band = true;
            if (!string.IsNullOrWhiteSpace(person.Phone))
                band = true;
            if (!band)
                return BadRequest("At least one field must be filled");

            PersonDTO p = uow.PersonRepository.GetByIdDTO(id);
            if (p == null)
                return NotFound();

            // Update fields if they are not null
            if (p.Name != person.Name && !string.IsNullOrWhiteSpace(person.Name))
                p.Name = person.Name;
            else
                p.Name = null;
            
            if (p.Email != person.Email && !string.IsNullOrWhiteSpace(person.Email))
                p.Email = person.Email;
            else
                p.Email = null;
            
            if (p.Phone != person.Phone && !string.IsNullOrWhiteSpace(person.Phone))
                p.Phone = person.Phone;
            else
                p.Phone = null;

            Person person_updated = uow.PersonRepository.Update(p);
            if (person_updated == null)
                return BadRequest("Error updating person");

            uow.Complete();
            return Ok();
        }

        [HttpDelete("{id}")] // Delete api/person/{id}
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            Person p = uow.PersonRepository.GetById(id);
            if (p == null)
                return NotFound();
            
            List<Loan> loans = uow.LoanRepository.GetLoansForPerson(p);

            if (loans.Count > 0)
                return BadRequest($"This person has {loans.Count} loans, it cannot be deleted");

            if (uow.PersonRepository.Delete(id))
            {
                uow.Complete();
                return Ok();
            }
            else
                return BadRequest("Error deleting person");
        }
    }
}
