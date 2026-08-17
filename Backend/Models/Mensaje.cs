namespace Backend.Models;

public class Mensaje
{
    public int Id { get; set; }

    public int ConversacionId { get; set; }

    public string Pregunta { get; set; } = string.Empty;

    public string Respuesta { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}
