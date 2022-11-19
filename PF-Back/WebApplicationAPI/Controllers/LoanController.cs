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
        
        [HttpPost] // POST api/loan
        public IActionResult Create([FromBody] Loan loan)
        {
            //Request validations
            if (loan is null)
                return BadRequest("Body is empty");
            if (loan.PersonId == 0)
                return BadRequest("Person Id is mandatory");
            if (loan.ThingId == 0)
                return BadRequest("Thing Id is mandatory");

            if (uow.PersonRepository.GetById(loan.PersonId) == null)
                return NotFound("Person not exist");

            // ThingRepository not implemented

            //if (uow.ThingRepository.GetById(loan.ThingId) == null)
            //    return NotFound("Thing not exist");

            loan.LoanDate = DateTime.Now;
            loan.ReturnDate = null;

            Loan l_created = uow.LoanRepository.Insert(loan);

            if (l_created == null)
                return BadRequest("Error creating loan");

            uow.Complete();

            return Created($"/loans/{l_created.Id}", l_created); // no me deja devolver el string
        }

        [HttpDelete("{id}")] // Delete api/loan/{id}
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            if (!uow.LoanRepository.Delete(id))
                return NotFound();

            uow.Complete();
            return Ok();
        }

        [HttpPut("{id}/setreturndate")] // SetReturnDate api/loan/{id}/setreturndate
        public IActionResult SetReturnDate(int id, [FromBody] Loan loan)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            if (loan is null)
                return BadRequest("Body is empty");

            if (loan.ReturnDate == null)
                return BadRequest("Return Date is mandatory");

            Loan l = uow.LoanRepository.GetById(id);

            if (l == null)
                return NotFound();

            Console.WriteLine(loan.ReturnDate);

            if (l.LoanDate >= loan.ReturnDate)
                return BadRequest("Return Date must be greater than Loan Date");

            l.ReturnDate = loan.ReturnDate;

            if(uow.LoanRepository.SetReturnDate(l))
            {
                uow.Complete();
                return Ok();
            }
            else
                return BadRequest("Error setting return date");
        }
    }
}
