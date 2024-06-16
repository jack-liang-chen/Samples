using Microsoft.AspNetCore.Mvc;
using ProducerService.Services;

namespace ProducerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController(RabbitMQService rabbitMQService) : ControllerBase
{
    private readonly RabbitMQService _rabbitMQService = rabbitMQService;

    [HttpPost]
    public IActionResult SendMessage([FromBody] string message)
    {
        _rabbitMQService.SendMessage(message);
        return Ok("Message sent to RabbitMQ");
    }
}
