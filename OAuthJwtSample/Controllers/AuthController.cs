using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthJwtSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController() : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        if (userLogin.Username != "test" || userLogin.Password != "password") 
            return Unauthorized();

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHereAndTheKeyByteMustMoreThan256");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, userLogin.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "https://auth.example.com",
            Audience = "https://api.example.com",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}
