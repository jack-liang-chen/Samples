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

    [HttpPost("login2")]
    public IActionResult Login2([FromBody] UserLogin userLogin)
    {
        if (userLogin.Username != "test" || userLogin.Password != "password")
            return Unauthorized();

        var accessToken = GenerateJwtToken(userLogin.Username, 1); // 1 hour
        var refreshToken = GenerateJwtToken(userLogin.Username, 24 * 7); // 1 week

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] TokenRequest tokenRequest)
    {
        var principal = GetPrincipalFromExpiredToken(tokenRequest.RefreshToken);
        
        if (principal == null)
            return Unauthorized();

        var username = principal.Identity.Name;
        var newAccessToken = GenerateJwtToken(username, 1); // 1 hour
        var newRefreshToken = GenerateJwtToken(username, 24 * 7); // 1 week

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }

    private static string GenerateJwtToken(string username, int hoursValid)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHereAndTheKeyByteMustMoreThan256");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(hoursValid),
            Issuer = "https://auth.example.com",
            Audience = "https://api.example.com",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsPrincipal GetPrincipalFromExpiredToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHereAndTheKeyByteMustMoreThan256");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // We check token expiration manually
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://auth.example.com",
            ValidAudience = "https://api.example.com",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class TokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
