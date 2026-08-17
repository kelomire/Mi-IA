namespace Backend.Models;

public class Conversacion
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
}
