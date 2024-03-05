using FusionStackBackEnd.Models;
using FusionStackBackEnd.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FusionStackBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        //Constructor base Injection 
        private readonly SignupRepositoryImpl signupRepo;
        private readonly LoginRepositoryImpl loginRepo;
        public RegisterController(SignupRepositoryImpl signupRepo, LoginRepositoryImpl loginRepo)
        {
            this.signupRepo = signupRepo ?? throw new ArgumentNullException(nameof(signupRepo));
            this.loginRepo = loginRepo;
        }

        //Registration Handler
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Register([FromBody] RegisterModelDto model)
            {
 
            Console.WriteLine(model.Email);
            try
            {
                if (model.Name == "" || model.Name==null )
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Name is null");
                }

                if (model.Email == "" || model.Email==null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Email is null");
                }
                if (model.Password == "" || model.Password==null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Password is null");
                }
                Console.WriteLine(model.Email);
                User newUser = new User();
                newUser.Name = model.Name;
                newUser.Email = model.Email;
                newUser.Password = model.Password;
                newUser.Phone=model.Phone;
                newUser.Id = model.Id;
               
                newUser.RoleId = loginRepo.getRole(model.Role).Id;
         
               
                signupRepo.AddUser(newUser);
    }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
                return Ok("Registration successful");
            }
     }

        public class RegisterModelDto
        {
           
            public string Email { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public int Id { get; set; }
    }
    }



