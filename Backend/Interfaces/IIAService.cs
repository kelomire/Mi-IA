namespace Backend.Interfaces;

public interface IIAService
{
    Task<string> ResponderAsync(
        string pregunta,
        string historial = "");
}
