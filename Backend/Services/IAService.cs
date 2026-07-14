using Backend.Interfaces;
using Backend.Providers;

namespace Backend.Services;

public class IAService : IIAService
{
    private readonly OllamaProvider _ollama;

    public IAService(OllamaProvider ollama)
    {
        _ollama = ollama;
    }

    public async Task<string> ResponderAsync(string pregunta)
    {
        return await _ollama.PreguntarAsync(pregunta);
    }
}