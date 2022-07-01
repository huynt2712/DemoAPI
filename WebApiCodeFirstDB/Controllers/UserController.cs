using BlogWebApi.Data;
using BlogWebApi.Entties;
using BlogWebApi.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogWebApi.Controllers
{
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
            //= new List<User>(); //Get data from database
            var userList = _blogDBContext.Users.Add(new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
            });
            _blogDBContext.SaveChanges();

            var user = _blogDBContext.Users.Where(x => x.UserName == model.UserName
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
    }
}
