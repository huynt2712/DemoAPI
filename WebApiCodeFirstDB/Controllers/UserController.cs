using BlogWebApi.Data;
using BlogWebApi.Entties;
using BlogWebApi.ViewModel.Common;
using BlogWebApi.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppSetting _appSetting;
        private readonly BlogDBContext _blogDBContext;

        public UserController(IOptions<AppSetting> appSetting, BlogDBContext blogDBContext)
        {
            _appSetting = appSetting.Value;
            _blogDBContext = blogDBContext;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _blogDBContext.Users.Where(x => x.UserName == model.UserName
            && x.Password == model.Password).FirstOrDefault();

            if (user == null)
                return Ok(new ApiResponseModel
                {
                    Message = "User Name or Password is incorrect. Please try again",
                    IsSuccess = false
                });

            return Ok(new ApiResponseModel
            {
                Data = GenerateToken(user),//token
                Message = "Login successfully",
                IsSuccess = true
            });
        }

        [Authorize]
        [HttpGet("UserInfo")]
        public IActionResult GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims?.ToList();

            if(claims != null)
                return Ok(claims[0]?.Value);
            return BadRequest();
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
    }
}
