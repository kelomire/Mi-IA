using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Interfaces;

namespace Backend.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IIAService _ia;

    public ChatController(IIAService ia)
    {
        _ia = ia;
    }

    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] Mensaje mensaje)
    {
        var respuesta = await _ia.ResponderAsync(mensaje.Pregunta);

        return Ok(new
        {
            respuesta
        });
    }
}