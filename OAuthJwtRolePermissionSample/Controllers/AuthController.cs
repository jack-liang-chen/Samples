using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthJwtRolePermissionSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        // 验证用户凭据
        if (userLogin.Username != "test" || userLogin.Password != "password")
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userLogin.Username),

                // 启用下面这行，就可以访问ProtectedController.GetPageX
                //new(ClaimTypes.Role, "Admin"), // 这里你可以根据实际情况设置用户角色
                new("permissions", "CanAccessResourceY"), // 添加具体权限声明
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, userLogin.Username),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHereAndTheKeyByteMustMoreThan256"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://auth.example.com",
            audience: "https://api.example.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}