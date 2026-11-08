using System.Collections.Generic;
using QuestPDF.Fluent;
using proyectoejerciciosescritorio.Infrastructure.Pdf;
using proyectoejerciciosescritorio.Models;

namespace proyectoejerciciosescritorio.Application.Services.Pdf;

public class PdfGeneratorService
{
    public byte[] GenerateRoutinePdf(IEnumerable<ItemRutina> items, IEnumerable<DiaRutina> dias, string? titulo = null)
    {
        string finalTitle = string.IsNullOrWhiteSpace(titulo) ? "Mi Plan de Entrenamiento" : titulo.Trim();
        var document = new RoutineDocument(items, dias, finalTitle);
        return document.GeneratePdf();
    }
}

