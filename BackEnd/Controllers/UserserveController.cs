using FusionStackBackEnd.Models;
using FusionStackBackEnd.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FusionStackBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserserveController: ControllerBase
    {
        private LoginRepositoryImpl repo;

        public UserserveController(LoginRepositoryImpl repo)
        {
            this.repo = repo;
        }

     
        
        [HttpGet]
        [Route("getUser/{page}/{pageSize}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetUserDetails(int page,int pageSize)
        {
            var user = repo.getUsers(page,pageSize);
            var PageCount=repo.pageCount();
            return Ok(new { user ,PageCount});
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update")]
        public IActionResult updateUser([FromBody] RegisterModelDto user)
        {
            try
            {
                repo.UpdateUser(user);
            }catch(Exception e)
            {
                return StatusCode(500,"Somthing went wrong");
            }
            return Ok(user);
        }

        [HttpDelete()]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult dropUser(int id)
        {
            Console.WriteLine(id);

            if (id == null)
                return BadRequest();
            try
            {
                repo.deleteUser(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();

        }
}}
    

