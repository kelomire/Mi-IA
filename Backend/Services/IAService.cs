using Backend.Interfaces;
using Backend.Providers;

namespace Backend.Services;

public class IAService : IIAService
{
    private readonly OllamaProvider _ollama = new();

    public async Task<string> ResponderAsync(string pregunta)
    {
        return await _ollama.PreguntarAsync(pregunta);
    }
}