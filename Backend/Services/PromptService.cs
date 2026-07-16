namespace Backend.Services;

public class PromptService
{
    private readonly string _ruta =
        Path.Combine(AppContext.BaseDirectory, "Prompts", "SystemPrompt.txt");

    public string ObtenerPrompt()
    {
        if (!File.Exists(_ruta))
            return "";

        return File.ReadAllText(_ruta);
    }
}
