using Backend.Configurations;
using Microsoft.Extensions.Options;

namespace Backend.Services;

public class PromptService
{
    private readonly AIConfiguration _config;

    public PromptService(IOptions<AIConfiguration> options)
    {
        _config = options.Value;
    }

    public string ObtenerPrompt()
    {
        var ruta = Path.Combine(
            AppContext.BaseDirectory,
            _config.SystemPrompt);

        if (!File.Exists(ruta))
            return "";

        return File.ReadAllText(ruta);
    }
}