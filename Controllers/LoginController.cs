using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Todo_List_ASPNETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest model)
        {
            // Logique pour enregistrer un nouvel utilisateur
            //var user = await _userService.RegisterAsync(model);
            //if (user == null)
            //    return BadRequest("Erreur lors de l'inscription.");

            return Ok("Inscription réussie.");
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Récupérer le profil de l'utilisateur authentifié
            //var userId = User.Identity.Name;
            //var profile = _userService.GetProfile(userId);
            return Ok("profile");
        }
    }
}
