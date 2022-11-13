using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DataAccess;
using Entities;
using WebApplicationAPI.DataAccess.Thing;

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

        [HttpGet] // GETAll api/person
        public IActionResult GetAll()
        {
            return Ok(uow.PersonRepository.GetAll()); // Esto va a llamar del generic repository al getAll de ahi
        }
        
        [HttpPost] // POST api/person
        public IActionResult Create([FromBody] Person person)
        {
            //Validaciones de request
            if (person is null
               || string.IsNullOrWhiteSpace(person.Name))
                return BadRequest("Description is mandatory");

            var a = uow.PersonRepository.Insert(person);

            //Todo salio bien, es un POST, asi que vamos a devolver CREATED
            return Created($"/categories/{a.Id}", a);
            //Ver en POSTMAN que el response tiene un header "Location"
        }
    }
}
