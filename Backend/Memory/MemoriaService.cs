namespace Backend.Memory;

public class MemoriaService
{
    private readonly List<string> _mensajes = new();

    public void Agregar(string rol, string mensaje)
    {
        _mensajes.Add($"{rol}: {mensaje}");

        if (_mensajes.Count > 10)
            _mensajes.RemoveAt(0);
    }

    public string ObtenerHistorial()
    {
        return string.Join("\n", _mensajes);
    }

    public void Limpiar()
    {
        _mensajes.Clear();
    }
}