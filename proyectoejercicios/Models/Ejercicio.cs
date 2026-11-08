namespace proyectoejercicios.Models;

public class Ejercicio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string RutaImagen { get; set; } = string.Empty;
    public string GrupoMuscular { get; set; } = "General";
    public string UrlExplicacion { get; set; } = string.Empty;
}

