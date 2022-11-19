using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DataAccess;
using Entities;

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
            return Ok(uow.PersonRepository.GetAll()); // Esto va a llamar del generic repository al getAll de ahi
        }

        [HttpGet("{id}")] // GetOne api/person/{id}
        public IActionResult GetOne(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
                return NotFound();
            
            return Ok(person);
        }

        [HttpPost] // POST api/person
        public IActionResult Create([FromBody] Person person)
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

            var p_created = uow.PersonRepository.Insert(person);

            if (p_created == null)
                return BadRequest("Error creating person");

            uow.Complete();
            
            return Created($"/person/{p_created.Id}", p_created); // no me deja devolver el string
        }

        [HttpDelete("{id}")] // Delete api/person/{id}
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            if (!uow.PersonRepository.Delete(id))
                return NotFound();
            
            uow.Complete();
            return Ok();
        }
    }
}
