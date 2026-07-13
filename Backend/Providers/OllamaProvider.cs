namespace Backend.Providers;

public class OllamaProvider
{
    public string Preguntar(string pregunta)
    {
        return $"Ollama responderá: {pregunta}";
    }
}