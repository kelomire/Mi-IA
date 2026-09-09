namespace Backend.Models;

public class ChatRequest
{
    public string Pregunta { get; set; } = string.Empty;

    public int? ConversacionId { get; set; }
}
