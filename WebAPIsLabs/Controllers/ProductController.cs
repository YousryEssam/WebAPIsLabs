using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using WebAPIsLabs.DTO;
using WebAPIsLabs.Models;
using WebAPIsLabs.Repos;

namespace WebAPIsLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        public ProductController(IProductRepository productRepository) {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(productRepository.GetAll());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) {
            Product product = productRepository.GetProductCategoryById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(new ProductWithCategoryName(product));
        }

        [HttpPost]
        public IActionResult AddNew(AddProduc productFromReq)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = productFromReq.Name;
                product.Description = productFromReq.Description;
                product.Price = productFromReq.Price;
                product.CategoryId = productFromReq.CategoryId;
                productRepository.Insert(product);
                productRepository.Save();
                return CreatedAtAction("GetById", new { id = product.Id }, product);
            }
            return BadRequest(ModelState);
        }
    }
}
