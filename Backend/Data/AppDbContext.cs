using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversacion>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Mensaje>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Mensaje>()
            .HasOne<Conversacion>()
            .WithMany()
            .HasForeignKey(m => m.ConversacionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
