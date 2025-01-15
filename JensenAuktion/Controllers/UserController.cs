using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public IActionResult InsertUser(User user)
        {
            _userRepo.InsertUser(user);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
            return Ok();
        }
    }
}

