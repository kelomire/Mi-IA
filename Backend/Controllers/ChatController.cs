using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Interfaces;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IIAService _ia = new IAService();

    [HttpPost]
    public IActionResult Chat([FromBody] Mensaje mensaje)
    {
        var respuesta = _ia.Responder(mensaje.Pregunta);

        return Ok(new
        {
            respuesta
        });
    }
}