namespace proyectoejercicios.Models;

public class ItemRutina
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int EjercicioId { get; set; }
    public Ejercicio? Ejercicio { get; set; }
    public string DiaId { get; set; } = string.Empty;
    public int Series { get; set; } = 3;
    public int Repeticiones { get; set; } = 10;
    public int OrdenVisual { get; set; }
    public bool EsEtiqueta { get; set; } = false;
    public string TextoEtiqueta { get; set; } = string.Empty;
}

