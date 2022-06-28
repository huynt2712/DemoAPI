using JsonWebToken.Entities;
using JsonWebToken.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JsonWebToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSetting _appSetting;

        public UserController(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var userList = new List<User>(); //Get data from database
            userList.Add(new Entities.User
            {
                Name = "Huy Nguyen",
                Password = "12345",
                Email = "HuyNguyen@gmail.com",
                Phone = "03997788",
                Id = 1,
                DateOfBirth = DateTime.Now,
                UserName = "HuyNguyen"
            });

            var user = userList.Where(x => x.UserName == model.UserName
            && x.Password == model.Password).FirstOrDefault();

            if (user == null) return NotFound();

            return Ok(GenerateToken(user)); //token
        }

        private string GenerateToken(User user)
        {
            //token: header, payload, signature
            //Secret key _appSetting.SecretKey

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.secretKey);

            var tokenParams = new SecurityTokenDescriptor
            {
                //Payload
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Phone", user.Phone)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5), //1h, 2h
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenParams);

            return jwtTokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("UserInfo")]
        public IActionResult GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToList();

            return Ok(claims[1]?.Value);
        }
    }
}
