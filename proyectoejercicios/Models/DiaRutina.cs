using System;

namespace proyectoejercicios.Models;

public class DiaRutina
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nombre { get; set; } = string.Empty;
}

