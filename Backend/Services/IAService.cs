using Backend.Interfaces;
using Backend.Memory;
using Backend.Providers;

namespace Backend.Services;

public class IAService : IIAService
{
    private readonly OllamaProvider _ollama;
    private readonly PromptService _promptService;
    private readonly MemoriaService _memoria;

    public IAService(
        OllamaProvider ollama,
        PromptService promptService,
        MemoriaService memoria)
    {
        _ollama = ollama;
        _promptService = promptService;
        _memoria = memoria;
    }

    public async Task<string> ResponderAsync(string pregunta)
    {
        var promptSistema = _promptService.ObtenerPrompt();

        _memoria.Agregar("Usuario", pregunta);

        var promptCompleto =
$"""
{promptSistema}

Conversación:
{_memoria.ObtenerHistorial()}

Mi-IA:
""";

        var respuesta = await _ollama.PreguntarAsync(promptCompleto);

        _memoria.Agregar("Mi-IA", respuesta);

        return respuesta;
    }
}