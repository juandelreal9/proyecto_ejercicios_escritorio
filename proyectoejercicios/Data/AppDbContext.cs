using Microsoft.EntityFrameworkCore;
using proyectoejercicios.Models;

namespace proyectoejercicios.Data;

public class AppDbContext : DbContext
{
    public DbSet<Ejercicio> Ejercicios { get; set; }

    public AppDbContext()
    {
        // Asegura que la base de datos y sus tablas estén creadas.
        Database.EnsureCreated();
        try
        {
            Database.ExecuteSqlRaw("ALTER TABLE Ejercicios ADD COLUMN GrupoMuscular TEXT NOT NULL DEFAULT 'General'");
        }
        catch
        {
            // La columna ya existe o la tabla se acaba de crear con ella
        }

        try
        {
            Database.ExecuteSqlRaw("ALTER TABLE Ejercicios ADD COLUMN UrlExplicacion TEXT NOT NULL DEFAULT ''");
        }
        catch
        {
            // La columna ya existe o la tabla se acaba de crear con ella
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // En una app .NET MAUI, DataDirectory se mapea a la carpeta local de la aplicación
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ejercicios.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}

