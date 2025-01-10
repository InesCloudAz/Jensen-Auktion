using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserRepo _userRepo;

        public UserController(UserRepo userRepo)
        {
            _userRepo = new UserRepo();
        }

        [HttpPost]
        public IActionResult InsertUser(User user)
        {
            _userRepo.InsertUser(user);
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
            return NoContent();
        }
    }
}

