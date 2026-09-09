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

    public async Task<string> ResponderAsync(
        string pregunta,
        string historial = "")
    {
        var promptSistema = _promptService.ObtenerPrompt();

        var historialCompleto = string.IsNullOrWhiteSpace(historial)
            ? _memoria.ObtenerHistorial()
            : historial;

        var promptCompleto =
$"""
{promptSistema}

Conversación anterior:
{historialCompleto}

Usuario:
{pregunta}

Lion-IA:
""";

        var respuesta = await _ollama.PreguntarAsync(promptCompleto);

        return respuesta;
    }
}
