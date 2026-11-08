using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;
using proyectoejerciciosescritorio.Models;

namespace proyectoejerciciosescritorio.Data;

public class AppDbContext : DbContext
{
    public DbSet<Ejercicio> Ejercicios { get; set; }

    public AppDbContext()
    {
        Database.EnsureCreated();
        try
        {
            Database.ExecuteSqlRaw("ALTER TABLE Ejercicios ADD COLUMN GrupoMuscular TEXT NOT NULL DEFAULT 'General'");
        }
        catch
        {
        }

        try
        {
            Database.ExecuteSqlRaw("ALTER TABLE Ejercicios ADD COLUMN UrlExplicacion TEXT NOT NULL DEFAULT ''");
        }
        catch
        {
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ejercicios_escritorio.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}

