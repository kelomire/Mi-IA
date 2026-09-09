using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Interfaces;
using Backend.Data;

namespace Backend.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IIAService _ia;
    private readonly AppDbContext _db;

    public ChatController(
        IIAService ia,
        AppDbContext db)
    {
        _ia = ia;
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Pregunta))
        {
            return BadRequest(new
            {
                mensaje = "La pregunta no puede estar vacía."
            });
        }

        Conversacion? conversacion;

        if (request.ConversacionId.HasValue)
        {
            conversacion = await _db.Conversaciones
                .Include(c => c.Mensajes)
                .FirstOrDefaultAsync(
                    c => c.Id == request.ConversacionId.Value);

            if (conversacion == null)
            {
                return NotFound(new
                {
                    mensaje = "La conversación no existe."
                });
            }
        }
        else
        {
            conversacion = new Conversacion();

            _db.Conversaciones.Add(conversacion);

            await _db.SaveChangesAsync();
        }

        var historial = string.Join(
            "\n",
            conversacion.Mensajes
                .OrderBy(m => m.Fecha)
                .Select(m =>
                    $"Usuario: {m.Pregunta}\n" +
                    $"Lion-IA: {m.Respuesta}")
        );

        var respuesta = await _ia.ResponderAsync(
            request.Pregunta,
            historial);

        var mensaje = new Mensaje
        {
            ConversacionId = conversacion.Id,
            Pregunta = request.Pregunta,
            Respuesta = respuesta
        };

        _db.Mensajes.Add(mensaje);

        await _db.SaveChangesAsync();

        return Ok(new ChatResponse
        {
            ConversacionId = conversacion.Id,
            Respuesta = respuesta
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerConversacion(int id)
    {
        var conversacion = await _db.Conversaciones
            .Include(c => c.Mensajes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conversacion == null)
        {
            return NotFound(new
            {
                mensaje = "La conversación no existe."
            });
        }

        return Ok(new
        {
            conversacionId = conversacion.Id,
            fechaCreacion = conversacion.FechaCreacion,
            mensajes = conversacion.Mensajes
                .OrderBy(m => m.Fecha)
                .Select(m => new
                {
                    m.Id,
                    m.Pregunta,
                    m.Respuesta,
                    m.Fecha
                })
        });
    }
}
