using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static Microsoft.OpenApi.Models.OpenApiInfo;

namespace MessageBoard.Models
{
  [Route("api/controller")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
      _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
      var user = Authenticate(userLogin);
      if (user != null)
      {
        var token = Generate(user);
        return Ok(token);
      }
      return NotFound("User not found");
    }

    private string Generate(AppUser user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new []
      {
        new Claim(ClaimTypes.NameIdentifier, user.Username),
        new Claim(ClaimTypes.Email, user.EmailAddress),
        new Claim(ClaimTypes.Role, user.Role)
      };

      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
      _config["Jwt:Audience"],
      claims,
      expires: DateTime.Now.AddMinutes(15),
      signingCredentials: credentials);

      return  new JwtSecurityTokenHandler().WriteToken(token);
    }

    private AppUser Authenticate(UserLogin userLogin)
    {
      var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() ==
      userLogin.Username.ToLower() && o.Password == userLogin.Password);

      if(currentUser != null)
      {
        return currentUser;
      }
      return null;
    }
  }
}