using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplicationAPI.DataAccess;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public CategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet] // GetAll api/category
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            return Ok(uow.CategoryRepository.GetAll());
        }

        [HttpGet("{id}")] // GetOne api/category/{id}
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetOne(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            Category category = uow.CategoryRepository.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost] // POST api/category
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Category category)
        {
            //Request validations
            if (category is null)
                return BadRequest("Body is empty");

            if (string.IsNullOrWhiteSpace(category.Description))
                return BadRequest("Description is mandatory");

            if (uow.CategoryRepository.GetByDescrpition(category.Description) != null)
                return BadRequest("Category already exists");

            category.CreationDate = DateTime.Now;
            Category c_created = uow.CategoryRepository.Insert(category);

            if (c_created == null)
                return BadRequest("Error creating category");

            uow.Complete();

            return Created($"/person/{c_created.Id}", c_created);
        }

        [HttpPut("{id}")] // PUT api/category/{id}
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            //Request validations
            if (category is null)
                return BadRequest("Body is empty");
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            // Body validations
            bool band = false;
            if (!string.IsNullOrWhiteSpace(category.Description))
                band = true;
            if (!string.IsNullOrWhiteSpace(category.CreationDate.ToString()))
                band = true;
            if (!band)
                return BadRequest("At least one field must be filled");

            Category c = uow.CategoryRepository.GetById(id);
            if (c == null)
                return NotFound();

            // Update fields if they are not null
            if (c.Description != category.Description && !string.IsNullOrWhiteSpace(category.Description))
                c.Description = category.Description;
            if (c.CreationDate != category.CreationDate && !string.IsNullOrWhiteSpace(category.CreationDate.ToString()))
                c.CreationDate = category.CreationDate;

            uow.CategoryRepository.Update(c);
            uow.Complete();

            return Ok(c);
        }

        [HttpDelete("{id}")] // Delete api/category/{id}
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("ID is mandatory, must be an integer and must be greater than 0");

            // Check the Things that have this category and delete them or change the category

            if (!uow.CategoryRepository.Delete(id))
                return NotFound();

            uow.Complete();
            return Ok();
        }
    }
}
