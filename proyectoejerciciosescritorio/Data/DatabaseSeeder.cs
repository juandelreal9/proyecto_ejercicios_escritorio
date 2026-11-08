using System;
using System.Collections.Generic;
using System.Linq;
using proyectoejerciciosescritorio.Models;

namespace proyectoejerciciosescritorio.Data;

public static class DatabaseSeeder
{
    public static readonly Ejercicio[] EjerciciosDefecto = new[]
    {
        // Pecho (12)
        new Ejercicio { Nombre = "Cruces en Poleas de Abajo hacia Arriba", RutaImagen = "cruces_en_poleas_de_abajo_hacia_arriba.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Fondos en Paralelo", RutaImagen = "fondos_en_paralelo.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas", RutaImagen = "lagartijas.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas Arqueras", RutaImagen = "lagartijas_arqueras.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Lagartijas Rodillas al Suelo", RutaImagen = "lagartijas_rodillas_al_suelo.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Peck Fly", RutaImagen = "peck_fly.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Cables", RutaImagen = "press_cables.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Declinado con Barra", RutaImagen = "press_declinado_con_barra.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Horizontal con Barra", RutaImagen = "press_horizontal_con_barra.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Inclinado con Barra", RutaImagen = "press_inclinado_con_barra.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Press Inclinado con Mancuerna", RutaImagen = "press_inclinado_con_mancuerna.jpg", GrupoMuscular = "Pecho" },
        new Ejercicio { Nombre = "Pullover con Mancuerna en Banco Plano", RutaImagen = "pullover_con_mancuerna_en_banco_plano.jpg", GrupoMuscular = "Pecho" },

        // Espalda (14)
        new Ejercicio { Nombre = "Australianas", RutaImagen = "australianas.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Dominada Comando", RutaImagen = "dominada_comando.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Dominadas", RutaImagen = "dominadas.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Hiperextensiones en Banco a 45 Grados", RutaImagen = "hiperextensiones_en_banco_a_45_grados.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Jalón al Frente", RutaImagen = "jalon_al_frente.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Jalón Cerrado", RutaImagen = "jalon_cerrado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Jalón Unilateral en Polea Alta", RutaImagen = "jalon_unilateral_en_polea_alta.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Pullover en Polea Alta Brazos Rectos", RutaImagen = "pullover_en_polea_alta_brazos_rectos.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo con Barra con Torso Inclinado", RutaImagen = "remo_con_barra_con_torso_inclinado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo con Mancuerna", RutaImagen = "remo_con_mancuerna.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo con Mancuernas en Banco Inclinado", RutaImagen = "remo_con_mancuernas_en_banco_inclinado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo en Máquina Sentado", RutaImagen = "remo_en_maquina_sentado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo Sentado", RutaImagen = "remo_sentado.jpg", GrupoMuscular = "Espalda" },
        new Ejercicio { Nombre = "Remo T Abierto", RutaImagen = "remo_t_abierto.jpg", GrupoMuscular = "Espalda" },

        // Piernas (27)
        new Ejercicio { Nombre = "Abductor", RutaImagen = "abductor.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Buenos Días con Barra", RutaImagen = "buenos_dias_con_barra.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplante Búlgaro", RutaImagen = "desplante_bulgaro.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplante en Reversa", RutaImagen = "desplante_en_reversa.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplantes Dobles", RutaImagen = "desplantes_dobles.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Desplantes Libres", RutaImagen = "desplantes_libres.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Elevación de Talones Sentado en Máquina", RutaImagen = "elevacion_de_talones_sentado_en_maquina.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Extensión de Cuádriceps", RutaImagen = "extension_de_cuadriceps.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Femoral Acostado", RutaImagen = "femoral_acostado.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Femoral Sentado", RutaImagen = "femoral_sentado.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Goblet Squat", RutaImagen = "goblet_squat.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Hip Thrust", RutaImagen = "hip_thrust.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Pantorrilla", RutaImagen = "pantorrilla.jpg", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Pantorrilla Individual", RutaImagen = "pantorrilla_individual.png", GrupoMuscular = "Piernas" },
        new Ejercicio { Nombre = "Patada de Glúteo en Polea", RutaImagen = "patada_de_gluteo_en_polea.jpg", GrupoMuscular = "Piernas" },
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

        // Hombros (30)
        new Ejercicio { Nombre = "Cruces en Poleas para Deltoides Posterior", RutaImagen = "cruces_en_poleas_para_deltoides_posterior.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevación Frontal", RutaImagen = "elevacion_frontal.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevación Frontal con Barra Recta", RutaImagen = "elevacion_frontal_con_barra_recta.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones en Y en Banco Inclinado", RutaImagen = "elevaciones_en_y_en_banco_inclinado.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Frontales Alternadas con Mancuernas", RutaImagen = "elevaciones_frontales_alternadas_con_mancuernas.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Frontales con Disco", RutaImagen = "elevaciones_frontales_con_disco.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Frontales en Banco Inclinado", RutaImagen = "elevaciones_frontales_en_banco_inclinado.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Frontales en Polea Baja con Cuerda", RutaImagen = "elevaciones_frontales_en_polea_baja_con_cuerda.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevación Lateral", RutaImagen = "elevacion_lateral.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales con Cable Inclinado", RutaImagen = "elevaciones_laterales_con_cable_inclinado.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales en Banco Inclinado", RutaImagen = "elevaciones_laterales_en_banco_inclinado.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales en Máquina", RutaImagen = "elevaciones_laterales_en_maquina.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales en Polea", RutaImagen = "elevaciones_laterales_en_polea.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales en Polea tras Espalda", RutaImagen = "elevaciones_laterales_en_polea_tras_espalda.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Elevaciones Laterales Lu Raises", RutaImagen = "elevaciones_laterales_lu_raises.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Encogimientos de Hombros", RutaImagen = "encogimientos_de_hombros.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Face Pull en Polea", RutaImagen = "face_pull_en_polea.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Militar Mancuerna", RutaImagen = "militar_mancuerna.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Pájaros con Cables en Polea Baja", RutaImagen = "pajaros_con_cables_en_polea_baja.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Pájaros con Mancuernas en Banco Inclinado", RutaImagen = "pajaros_con_mancuernas_en_banco_inclinado.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Pájaros de Pie con Mancuernas", RutaImagen = "pajaros_de_pie_con_mancuernas.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Pájaros en Máquina Pec Deck Invertida", RutaImagen = "pajaros_en_maquina_pec_deck_invertida.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Arnold", RutaImagen = "press_arnold.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press de Hombros en Máquina Smith", RutaImagen = "press_de_hombros_en_maquina_smith.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Militar con Barra", RutaImagen = "press_militar_con_barra.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Militar de Pie con Mancuernas", RutaImagen = "press_militar_de_pie_con_mancuernas.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Militar Sentado con Mancuernas", RutaImagen = "press_militar_sentado_con_mancuernas.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Press Militar Unilateral con Mancuerna", RutaImagen = "press_militar_unilateral_con_mancuerna.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Remo al Mentón con Barra", RutaImagen = "remo_al_menton_con_barra.jpg", GrupoMuscular = "Hombros" },
        new Ejercicio { Nombre = "Remo al Mentón en Polea Baja", RutaImagen = "remo_al_menton_en_polea_baja.jpg", GrupoMuscular = "Hombros" },

        // Bíceps (14)
        new Ejercicio { Nombre = "Concentrado", RutaImagen = "concentrado.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl 21s", RutaImagen = "curl_21s.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Araña", RutaImagen = "curl_arana.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Bayesiano en Polea", RutaImagen = "curl_bayesiano_en_polea.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl con Barra Recta", RutaImagen = "curl_con_barra_recta.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl en Polea Baja con Barra Recta", RutaImagen = "curl_en_polea_baja_con_barra_recta.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Inclinado con Mancuernas", RutaImagen = "curl_inclinado_con_mancuernas.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Mancuerna Alternado", RutaImagen = "curl_mancuerna_alternado.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Martillo Cruzado al Pecho", RutaImagen = "curl_martillo_cruzado_al_pecho.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Martillo en Banco Scott", RutaImagen = "curl_martillo_en_banco_scott.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Predicador con Mancuerna a una Mano", RutaImagen = "curl_predicador_con_mancuerna_a_una_mano.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Curl Zottman", RutaImagen = "curl_zottman.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Doble Bíceps en Polea", RutaImagen = "doble_biceps_en_polea.jpg", GrupoMuscular = "Bíceps" },
        new Ejercicio { Nombre = "Predicador Cerrado", RutaImagen = "predicador_cerrado.jpg", GrupoMuscular = "Bíceps" },

        // Tríceps (14)
        new Ejercicio { Nombre = "Copa", RutaImagen = "copa.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Copa a una Mano", RutaImagen = "copa_a_una_mano.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Extensión Cruzada en Poleas Dobles", RutaImagen = "extension_cruzada_en_poleas_dobles.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Extensión en Barra", RutaImagen = "extension_en_barra.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Extensión tras Nuca en Polea Baja", RutaImagen = "extension_tras_nuca_en_polea_baja.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Extensión Trenza", RutaImagen = "extension_trenza.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Extensión Unilateral en Polea sin Maneral", RutaImagen = "extension_unilateral_en_polea_sin_maneral.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Fondos en Banca", RutaImagen = "fondos_en_banca.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Lagartijas Diamante", RutaImagen = "lagartijas_diamante.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Patada de Tríceps con Mancuerna", RutaImagen = "patada_de_triceps_con_mancuerna.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Patada de Tríceps en Polea", RutaImagen = "patada_de_triceps_en_polea.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Press de Banca con Agarre Cerrado", RutaImagen = "press_de_banca_con_agarre_cerrado.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Press Francés con Barra Z", RutaImagen = "press_frances_con_barra_z.jpg", GrupoMuscular = "Tríceps" },
        new Ejercicio { Nombre = "Press Francés con Mancuernas", RutaImagen = "press_frances_con_mancuernas.jpg", GrupoMuscular = "Tríceps" },

        // Antebrazos (9)
        new Ejercicio { Nombre = "Cuelgue en Barra con Toalla", RutaImagen = "cuelgue_en_barra_con_toalla.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Curl de Muñeca con Barra en Banco", RutaImagen = "curl_de_muneca_con_barra_en_banco.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Curl de Muñeca con Mancuernas en Banco", RutaImagen = "curl_de_muneca_con_mancuernas_en_banco.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Curl de Muñeca en Polea Baja", RutaImagen = "curl_de_muneca_en_polea_baja.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Curl de Muñeca tras la Espalda con Barra", RutaImagen = "curl_de_muneca_tras_la_espalda_con_barra.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Desviación Radial y Cubital", RutaImagen = "desviacion_radial_y_cubital.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Pellizco de Discos", RutaImagen = "pellizco_de_discos.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Pronación y Supinación con Mancuerna", RutaImagen = "pronacion_y_supinacion_con_mancuerna.jpg", GrupoMuscular = "Antebrazos" },
        new Ejercicio { Nombre = "Rodillo de Muñeca con Cuerda y Disco", RutaImagen = "rodillo_de_muneca_con_cuerda_y_disco.jpg", GrupoMuscular = "Antebrazos" },

        // Core (21)
        new Ejercicio { Nombre = "Abdominales en V", RutaImagen = "abdominales_en_v.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Crunch Bicicleta", RutaImagen = "crunch_bicicleta.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Crunch en Fitball", RutaImagen = "crunch_en_fitball.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Crunch en Máquina", RutaImagen = "crunch_en_maquina.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Crunch en Polea Alta", RutaImagen = "crunch_en_polea_alta.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Dragon Flag", RutaImagen = "dragon_flag.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Elevación de Piernas Colgado en Barra", RutaImagen = "elevacion_de_piernas_colgado_en_barra.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Elevación de Piernas en Banco Plano", RutaImagen = "elevacion_de_piernas_en_banco_plano.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Elevación de Rodillas en Paralelas", RutaImagen = "elevacion_de_rodillas_en_paralelas.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Encogimientos Abdominales", RutaImagen = "encogimientos_abdominales.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Flexión Lateral con Mancuerna", RutaImagen = "flexion_lateral_con_mancuerna.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Giros Rusos", RutaImagen = "giros_rusos.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Hollow Body Hold", RutaImagen = "hollow_body_hold.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Leñador en Polea", RutaImagen = "lenador_en_polea.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Limpiaparabrisas", RutaImagen = "limpiaparabrisas.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Pies a la Barra", RutaImagen = "pies_a_la_barra.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Plancha Frontal Clásica", RutaImagen = "plancha_frontal_clasica.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Plancha Lateral", RutaImagen = "plancha_lateral.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Press Pallof en Polea", RutaImagen = "press_pallof_en_polea.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Rueda Abdominal", RutaImagen = "rueda_abdominal.jpg", GrupoMuscular = "Core" },
        new Ejercicio { Nombre = "Tijeras Abdominales", RutaImagen = "tijeras_abdominales.jpg", GrupoMuscular = "Core" },

        // Cuerpo Completo (1)
        new Ejercicio { Nombre = "Camino del Granjero", RutaImagen = "camino_del_granjero.jpg", GrupoMuscular = "Cuerpo Completo" }
    };

    public static void Initialize(AppDbContext context)
    {
        var existentes = context.Ejercicios.ToList();
        bool huboCambios = false;

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

                if (string.IsNullOrWhiteSpace(existente.GrupoMuscular) ||
                    existente.GrupoMuscular == "General" ||
                    existente.GrupoMuscular.Equals("Brazos", StringComparison.OrdinalIgnoreCase))
                {
                    existente.GrupoMuscular = seed.GrupoMuscular;
                    huboCambios = true;
                }
            }
        }

        // Si quedaron ejercicios personalizados en el antiguo grupo "Brazos", reclasificarlos para eliminar "Brazos"
        foreach (var restante in context.Ejercicios.Where(e => e.GrupoMuscular != null && e.GrupoMuscular.ToLower() == "brazos").ToList())
        {
            var nombre = restante.Nombre.ToLowerInvariant();
            if (nombre.Contains("muñeca") || nombre.Contains("muneca") || nombre.Contains("antebrazo") || nombre.Contains("radial") || nombre.Contains("pronaci") || nombre.Contains("pellizco") || nombre.Contains("toalla"))
                restante.GrupoMuscular = "Antebrazos";
            else if (nombre.Contains("copa") || nombre.Contains("extensi") || nombre.Contains("fondo") || nombre.Contains("tríceps") || nombre.Contains("triceps") || nombre.Contains("francés") || nombre.Contains("frances") || nombre.Contains("diamante") || nombre.Contains("cerrado"))
                restante.GrupoMuscular = "Tríceps";
            else
                restante.GrupoMuscular = "Bíceps";

            huboCambios = true;
        }

        if (huboCambios)
        {
            context.SaveChanges();
        }
    }
}

