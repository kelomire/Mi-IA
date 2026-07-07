namespace Backend.Services;

public class IAService
{
    public string Responder(string pregunta)
    {
        return $"Tu escribiste: {pregunta}";
    }
}