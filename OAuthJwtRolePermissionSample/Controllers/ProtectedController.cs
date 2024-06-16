using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthJwtRolePermissionSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet("pageX")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetPageX()
    {
        return Ok(new { Message = "This is Page X. Admin only." });
    }

    [HttpGet("resourceY")]
    [Authorize(Policy = "CanAccessResourceY")]
    public IActionResult GetResourceY()
    {
        return Ok(new { Message = "This is Resource Y. Authorized users only." });
    }
}
