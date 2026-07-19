namespace Backend.Configurations;

public class AIConfiguration
{
    public string Url { get; set; } = "";
    public string Model { get; set; } = "";
    public double Temperature { get; set; } = 0.7;
    public string SystemPrompt { get; set; } = "";
}