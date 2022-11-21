using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplicationAPI.DataAccess;
using WebApplicationAPI.Handlres;
using WebApplicationAPI.Helpers;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtHandler jwtHandler;
        private readonly IUnitOfWork uow;

        public AccountController(IUnitOfWork uow, IJwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
            this.uow = uow;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Person person)
        {
            if (person == null)
                return BadRequest("Body is empty");

            if (string.IsNullOrWhiteSpace(person.Email))
                return BadRequest("Email is mandatory");
            if (string.IsNullOrWhiteSpace(person.Password))
                return BadRequest("Password is mandatory");

            Person p = uow.PersonRepository.GetByEmail(person);

            if (p == null)
                return NotFound("Email or password is incorrect");

            if (Hash.HashPassword(person.Password) != p.Password)
                return NotFound("Email or password is incorrect");

            Console.WriteLine("Login successfull");

            var bearer = jwtHandler.GenerateToken(p);
            return Ok(new
            {
                token = bearer,
            });
        }
    }
}
