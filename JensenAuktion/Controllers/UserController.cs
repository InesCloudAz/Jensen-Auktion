using JensenAuktion.Repository.DTO;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JensenAuktion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepo _userRepo;
        private readonly UserService _userService;

        public UserController(IUserRepo userRepo, UserService userService)
        {
            _userRepo = userRepo;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var member = _userRepo.GetAllUsers();

            return Ok(member);
        }

        [HttpPost]
        public IActionResult InsertUser(User user)
        {
            _userRepo.InsertUser(user);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {

            // Skriv ut alla claims för felsökning
            var claims = HttpContext.User.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            // Hämta UserID från token
            var userIdFromToken = User.FindFirstValue(ClaimTypes.Sid);

            //if (string.IsNullOrEmpty(userIdFromToken) || user.UserID.ToString() != userIdFromToken)

            if (string.IsNullOrEmpty(userIdFromToken))
            {
                return Unauthorized("Du kan bara uppdatera din egen profil.");
            }

            // Konvertera UserID från token till int
            if (!int.TryParse(userIdFromToken, out var userId))
            {
                return BadRequest("Ogiltigt UserID i token.");
            }

            var user = new User
            {
                UserID = userId,
                UserName = updateUserDto.UserName,
                Password = updateUserDto.Password
            };
            // Sätt UserID i användarobjektet som skickas med i body
            //user.UserID = userId;


            _userRepo.UpdateUser(user);
            return Ok(new { Message = "Profil uppdaterad!" });
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserLoginDTO loginDTO)
        {
            var userId = _userRepo.LoginUser(loginDTO.UserName, loginDTO.Password);

            if (userId != null)
            {
                //var token = _userService.GenerateToken(user);
                var user = new User { UserName = loginDTO.UserName, Password = loginDTO.Password, UserID = userId.Value };
                var token = _userService.GenerateToken(user);

                return Ok(new
                {
                    Message = "Inloggning lyckades!",
                    Token = token ,
                    UserID = userId
                });
            }
            else
            {
                return Unauthorized("Fel användarnamn eller lösenord.");
            }
        }
    }
}

