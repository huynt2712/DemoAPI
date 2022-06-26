using JWTToken.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly AppSetting _appSetting;

        public UserController(IOptionsMonitor<AppSetting> options)
        {
            _appSetting = options.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult ValidateLogin(LoginModel model)
        {
            var user = new User //Get from database
            {
                UserName = "Huy",
                Password = "12345",
                Email = "Huy@gmail.com",
                Id = 1,
                Name = "Huy Nguyen"
            };

            if (user == null)
                return NotFound();

            if (model.UserName == user.UserName && model.Password == user.Password)
            {
                return Ok(new ApiResponseModel
                {
                    Data = GenerateToken(user),
                    Message = "Login succesfully",
                    IsSuccess = true
                });
                //return Ok(GenerateToken(user)); //token
            }

            return BadRequest();
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor //claims - payload
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),

                    //roles

                     new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token); //tra ve chuoi string
        }

        [Authorize]
        [HttpGet("userInfo")]
        public IActionResult GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            var userName = claims[0].Value;
            return Ok(userName);
        }
    }
}
