using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Backend.Providers;

public class OllamaProvider
{
    private readonly HttpClient _httpClient = new();

    public async Task<string> PreguntarAsync(string pregunta)
    {
        var request = new
        {
            model = "gemma3:1b",
            prompt = pregunta,
            stream = false
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "http://localhost:11434/api/generate",
            contenido);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var documento = JsonDocument.Parse(json);

        return documento.RootElement
                        .GetProperty("response")
                        .GetString() ?? "";
    }
}