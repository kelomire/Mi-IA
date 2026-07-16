using Backend.Interfaces;
using Backend.Providers;

namespace Backend.Services;

public class IAService : IIAService
{
    private readonly OllamaProvider _ollama;
    private readonly PromptService _promptService;

    public IAService(
        OllamaProvider ollama,
        PromptService promptService)
    {
        _ollama = ollama;
        _promptService = promptService;
    }

    public async Task<string> ResponderAsync(string pregunta)
    {
        var promptSistema = _promptService.ObtenerPrompt();

        var promptCompleto =
$"""
{promptSistema}

Usuario:
{pregunta}
""";

        return await _ollama.PreguntarAsync(promptCompleto);
    }
}