using proyectoejercicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proyectoejercicios.Data;

public static class DatabaseSeeder
{
    private static readonly HashSet<string> RutasEjemploAntiguas = new(StringComparer.OrdinalIgnoreCase)
    {
        "press_banca.png",
        "fondos.png",
        "flexiones.png",
        "remo_barra.png",
        "dominadas.png",
        "peso_muerto.png",
        "muscle_up.png",
        "back_lever.png",
        "sentadilla_libre.png",
        "box_jumps.png",
        "kettlebell_swing.png",
        "wall_balls.png",
        "press_militar.png",
        "pino.png",
        "curl_biceps.png",
        "extension_triceps.png",
        "battle_ropes.png",
        "front_lever.png",
        "farmer_walk.png",
        "burpees.png",
        "thrusters.png"
    };

    private static readonly HashSet<string> NombresEjemploAntiguos = new(StringComparer.OrdinalIgnoreCase)
    {
        "Press de Banca",
        "Fondos en Paralelas (Dips)",
        "Flexiones de Brazo (Push-ups)",
        "Remo con Barra",
        "Dominadas (Pull-ups)",
        "Muscle-Up",
        "Back Lever",
        "Box Jumps",
        "Kettlebell Swing",
        "Wall Balls",
        "Press Militar",
        "Pino (Handstand)",
        "Curl de Bíceps",
        "Extensión de Tríceps",
        "Battle Ropes",
        "Front Lever",
        "Paseo del Granjero",
        "Burpees",
        "Thrusters"
    };

    public static readonly Ejercicio[] EjerciciosDefecto = new[]
    {
        // Pecho (9)
        new Ejercicio { Nombre = "Fondos en Paralelo", RutaImagen = "fondos_en_paralelo.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas", RutaImagen = "lagartijas.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas Arqueras", RutaImagen = "lagartijas_arqueras.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas Rodillas al Suelo", RutaImagen = "lagartijas_rodillas_al_suelo.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Peck Fly", RutaImagen = "peck_fly.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Cables", RutaImagen = "press_cables.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Horizontal con Barra", RutaImagen = "press_horizontal_con_barra.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Inclinado con Barra", RutaImagen = "press_inclinado_con_barra.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Inclinado con Mancuerna", RutaImagen = "press_inclinado_con_mancuerna.jpg", GrupoMuscular = "Pecho" },

        // Espalda (8)
        new Ejercicio { Nombre = "Australianas", RutaImagen = "australianas.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Dominada Comando", RutaImagen = "dominada_comando.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Dominadas", RutaImagen = "dominadas.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Jalón al Frente", RutaImagen = "jalon_al_frente.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Jalón Cerrado", RutaImagen = "jalon_cerrado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo con Mancuerna", RutaImagen = "remo_con_mancuerna.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo Sentado", RutaImagen = "remo_sentado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo T Abierto", RutaImagen = "remo_t_abierto.jpg", GrupoMuscular = "Espalda" },

        // Piernas (23)
        new Ejercicio { Nombre = "Abductor", RutaImagen = "abductor.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplante Búlgaro", RutaImagen = "desplante_bulgaro.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplantes Dobles", RutaImagen = "desplantes_dobles.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplantes Libres", RutaImagen = "desplantes_libres.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Extensión de Cuádriceps", RutaImagen = "extension_de_cuadriceps.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Femoral Acostado", RutaImagen = "femoral_acostado.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Femoral Sentado", RutaImagen = "femoral_sentado.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Goblet Squat", RutaImagen = "goblet_squat.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Hip Thrust", RutaImagen = "hip_thrust.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Pantorrilla", RutaImagen = "pantorrilla.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Pantorrilla Individual", RutaImagen = "pantorrilla_individual.png", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Patada Lateral", RutaImagen = "patada_lateral.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Peso Muerto", RutaImagen = "peso_muerto.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Peso Muerto con Mancuernas", RutaImagen = "peso_muerto_con_mancuernas.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Prensa de Piernas", RutaImagen = "prensa_de_piernas.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla con Barra", RutaImagen = "sentadilla_con_barra.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Estática", RutaImagen = "sentadilla_estatica.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Hack", RutaImagen = "sentadilla_hack.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Libre", RutaImagen = "sentadilla_libre.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Media", RutaImagen = "sentadilla_media.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Profunda en Smith", RutaImagen = "sentadilla_profunda_en_smith.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Sissy", RutaImagen = "sentadilla_sissy.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Sentadilla Sumo", RutaImagen = "sentadilla_sumo.jpg", GrupoMuscular = "Piernas" },

        // Hombros (5)
        new Ejercicio { Nombre = "Elevación Frontal", RutaImagen = "elevacion_frontal.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevación Lateral", RutaImagen = "elevacion_lateral.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Encogimientos de Hombros", RutaImagen = "encogimientos_de_hombros.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Militar Mancuerna", RutaImagen = "militar_mancuerna.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Arnold", RutaImagen = "press_arnold.jpg", GrupoMuscular = "Hombros" },

        // Brazos (11)
        new Ejercicio { Nombre = "Concentrado", RutaImagen = "concentrado.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Copa", RutaImagen = "copa.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Copa a una Mano", RutaImagen = "copa_a_una_mano.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Curl 21s", RutaImagen = "curl_21s.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Curl Mancuerna Alternado", RutaImagen = "curl_mancuerna_alternado.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Doble Bíceps en Polea", RutaImagen = "doble_biceps_en_polea.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Extensión en Barra", RutaImagen = "extension_en_barra.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Extensión Trenza", RutaImagen = "extension_trenza.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Fondos en Banca", RutaImagen = "fondos_en_banca.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Lagartijas Diamante", RutaImagen = "lagartijas_diamante.jpg", GrupoMuscular = "Brazos" },
        new Ejercicio { Nombre = "Predicador Cerrado", RutaImagen = "predicador_cerrado.jpg", GrupoMuscular = "Brazos" },

        // Core y Cuerpo Completo (2)
        new Ejercicio { Nombre = "Encogimientos Abdominales", RutaImagen = "encogimientos_abdominales.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Camino del Granjero", RutaImagen = "camino_del_granjero.jpg", GrupoMuscular = "Cuerpo Completo" }
    };

    public static void Initialize(AppDbContext context)
    {
        var existentes = context.Ejercicios.ToList();
        bool huboCambios = false;

        // 1. Eliminar los ejercicios de ejemplo anteriores si existen en la base de datos
        var paraEliminar = existentes
            .Where(e =>
                (!string.IsNullOrWhiteSpace(e.RutaImagen) && RutasEjemploAntiguas.Contains(e.RutaImagen.Trim())) ||
                NombresEjemploAntiguos.Contains(e.Nombre.Trim()))
            .ToList();

        if (paraEliminar.Count > 0)
        {
            context.Ejercicios.RemoveRange(paraEliminar);
            context.SaveChanges();
            existentes = context.Ejercicios.ToList();
        }

        // 2. Insertar o sincronizar los ejercicios migrados desde la carpeta de imágenes
        foreach (var seed in EjerciciosDefecto)
        {
            var existente = existentes.FirstOrDefault(e =>
                e.Nombre.Equals(seed.Nombre, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrWhiteSpace(e.RutaImagen) && e.RutaImagen.Equals(seed.RutaImagen, StringComparison.OrdinalIgnoreCase)));

            if (existente == null)
            {
                context.Ejercicios.Add(new Ejercicio
                {
                    Nombre = seed.Nombre,
                    RutaImagen = seed.RutaImagen,
                    GrupoMuscular = seed.GrupoMuscular,
                    UrlExplicacion = seed.UrlExplicacion
                });
                huboCambios = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(existente.RutaImagen) || existente.RutaImagen != seed.RutaImagen)
                {
                    existente.RutaImagen = seed.RutaImagen;
                    huboCambios = true;
                }

                if (string.IsNullOrWhiteSpace(existente.GrupoMuscular) || existente.GrupoMuscular == "General")
                {
                    existente.GrupoMuscular = seed.GrupoMuscular;
                    huboCambios = true;
                }
            }
        }

        if (huboCambios)
        {
            context.SaveChanges();
        }
    }
}
