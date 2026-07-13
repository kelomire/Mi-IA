using Backend.Interfaces;
using Backend.Providers;

namespace Backend.Services;

public class IAService : IIAService
{
    private readonly OllamaProvider _ollama = new();

    public string Responder(string pregunta)
    {
        return _ollama.Preguntar(pregunta);
    }
}