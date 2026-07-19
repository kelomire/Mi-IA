using System.Text;
using System.Text.Json;
using Backend.Configurations;
using Microsoft.Extensions.Options;

namespace Backend.Providers;

public class OllamaProvider
{
    private readonly HttpClient _httpClient;
    private readonly AIConfiguration _config;

    public OllamaProvider(HttpClient httpClient, IOptions<AIConfiguration> options)
    {
        _httpClient = httpClient;
        _config = options.Value;
    }

    public async Task<string> PreguntarAsync(string pregunta)
    {
        var request = new
        {
            model = _config.Model,
            prompt = pregunta,
            stream = false,
            options = new
            {
                temperature = _config.Temperature
            }
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            $"{_config.Url}/api/generate",
            contenido);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var documento = JsonDocument.Parse(json);

        return documento.RootElement
            .GetProperty("response")
            .GetString() ?? "";
    }
}