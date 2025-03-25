using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIsLabs.DTO;
using WebAPIsLabs.Models;
using WebAPIsLabs.Repos;

namespace WebAPIsLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoyController : ControllerBase
    {
        CategoryRepository categoryRepository;
        public CategoyController(CategoryRepository categoryRepository) {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryRepository.GetAll());
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id) {
            Category category = categoryRepository.GetById(id);
            if (category != null) {
                return Ok(category);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("wp/{id:int}")]
        public IActionResult GetWithProductsById(int id)
        {
            CategoryWithProductsName repos = categoryRepository.GetWithProductsById(id);
            return Ok(repos);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        public IActionResult GetByName(string name) {
            Category category = categoryRepository.GetByName(name);
            if (category != null) {
                return Ok(category);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Insert(Category category) {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(category);
                categoryRepository.Save();
                return CreatedAtAction("GetById", new { id = category.Id }, category);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id , Category categoryFromReq) {
            if (ModelState.IsValid) {
                Category category = categoryRepository.GetById(id);
                if (category != null) {
                    category.Name = categoryFromReq.Name;
                    category.Description = categoryFromReq.Description;

                    categoryRepository.Update(category);
                    categoryRepository.Save();
                    return NoContent();
                }
                ModelState.AddModelError("", "Invalid id");
            }
            return BadRequest(ModelState);
        }
        [HttpPut ("/newUpdate")]
        public IActionResult Update2(int id , Category categoryFromReq) {
            if (ModelState.IsValid) {
                Category category = categoryRepository.GetById(id);
                if (category != null) {
                    category.Name = categoryFromReq.Name;
                    category.Description = categoryFromReq.Description;

                    categoryRepository.Update(category);
                    categoryRepository.Save();
                    return NoContent();
                }
                ModelState.AddModelError("", "Invalid id");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) {
            Category category = categoryRepository.GetById(id);
            if (category != null) { 
                categoryRepository.Delete(id);
                categoryRepository.Save();
                return NoContent();
            }
            return BadRequest("Invalid Id");
        }
    }
}
