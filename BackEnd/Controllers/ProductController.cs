using FusionStackBackEnd.Models;
using FusionStackBackEnd.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FusionStackBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductRepository repo;
        public ProductController(ProductRepository repo)
        {
            this.repo = repo;
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        [Route("addProduct")]
        public IActionResult addProduct([FromBody] Product prod)
        {


            if (prod == null)
            {
                return StatusCode(500,"Enter Corrected Product");
            }
            try { repo.AddProd(prod); }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

           
            return Ok(prod);

        }
        [HttpGet]
        [Route("getProduct/{page}/{pageSize}")]
        public IActionResult getProduct(int page,int pageSize)
        {
            var product=repo.GetProd(page,pageSize);
            var pageCount = repo.PageCount();
            return Ok(new { product , pageCount });
        }

        [HttpDelete]
        [Route("delete/{productId}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult dropProduct(int productId)
        {
            Console.WriteLine(productId);
            
            if(productId == null)
            return BadRequest();
            try
            {
                repo.deleteProduct(productId);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();

        }
        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        [Route("update")]
        public IActionResult updateProduct([FromBody] Product product)
        {
            try
            {
                repo.UpdateProduct(product);
            }
            catch (Exception e)
            {
                return StatusCode(500,"somthing went wrong");
            }
                return Ok(product);
        }

    }
}
