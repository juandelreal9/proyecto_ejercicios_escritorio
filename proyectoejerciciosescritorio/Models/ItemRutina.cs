using System;

namespace proyectoejerciciosescritorio.Models;

public class ItemRutina
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int EjercicioId { get; set; }
    public Ejercicio? Ejercicio { get; set; }
    public string DiaId { get; set; } = string.Empty;
    public int Series { get; set; } = 4;
    public int Repeticiones { get; set; } = 12;
    public int OrdenVisual { get; set; }
    public bool EsEtiqueta { get; set; } = false;
    public string TextoEtiqueta { get; set; } = string.Empty;
}

