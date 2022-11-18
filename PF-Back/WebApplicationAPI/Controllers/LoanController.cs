using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DataAccess;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public LoanController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet] // GetAll api/loan
        public IActionResult GetAll()
        {
            List<Loan> loans = uow.LoanRepository.GetAll();
            foreach (Loan loan in loans)
                loan.Person = getPerson(loan.Id);
            return Ok(loans.ToList());
        }

        [HttpGet("{id}")] // GetOne api/loan/{id}
        public IActionResult GetOne(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            Loan loan = uow.LoanRepository.GetById(id);
            
            if (loan == null)
                return NotFound();

            loan.Person = getPerson(loan.Id);
            return Ok(loan);
        }

        private Person getPerson(int id)
        {
            Person person = uow.PersonRepository.GetById(id);
            if (person == null)
                return null;
            return person;
        }

        //[HttpPost] // POST api/person
        //public IActionResult Create([FromBody] Person person)
        //{
        //    Validaciones de request
        //    if (person is null)
        //        return BadRequest("Body is empty");

        //    if (string.IsNullOrWhiteSpace(person.Name))
        //        return BadRequest("Name is mandatory");
        //    if (string.IsNullOrWhiteSpace(person.Email))
        //        return BadRequest("Email is mandatory");
        //    if (string.IsNullOrWhiteSpace(person.Phone))
        //        return BadRequest("Phone is mandatory");

        //    var p_created = uow.PersonRepository.Insert(person);

        //    if (p_created == null)
        //        return BadRequest("Error creating person");

        //    uow.Complete(); // Esto va a llamar del generic repository al saveChanges de ahi

        //    return Created($"/categories/{p_created.Id}", p_created); // no me deja devolver el string
        //}

        [HttpDelete("{id}")] // Delete api/loan/{id}
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            bool deleted = uow.LoanRepository.Delete(id);
            if (!deleted)
                return NotFound();

            uow.Complete();
            return Ok();
        }
    }
}
