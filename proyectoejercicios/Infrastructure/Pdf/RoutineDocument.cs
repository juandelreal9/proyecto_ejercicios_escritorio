#if WINDOWS || MACCATALYST
using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using proyectoejercicios.Models;
using proyectoejercicios.Services;

using IContainer = QuestPDF.Infrastructure.IContainer;
using Colors = QuestPDF.Helpers.Colors;

namespace proyectoejercicios.Infrastructure.Pdf;

public class RoutineDocument : IDocument
{
    private readonly IEnumerable<ItemRutina> _items;
    private readonly IEnumerable<DiaRutina> _dias;
    private readonly string _titulo;

    public RoutineDocument(IEnumerable<ItemRutina> items, IEnumerable<DiaRutina> dias, string? titulo = null)
    {
        _items = items;
        _dias = dias;
        _titulo = string.IsNullOrWhiteSpace(titulo) ? "Mi Plan de Entrenamiento" : titulo.Trim();
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(_titulo).FontSize(24).SemiBold().FontColor(Colors.Blue.Darken2);
                column.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy}").FontSize(10).FontColor(Colors.Blue.Darken1);
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        var dayList = _dias.ToList();
        
        var groupedItems = _items
            .GroupBy(i => i.DiaId)
            .Select(g => new { 
                Dia = dayList.FirstOrDefault(d => d.Id == g.Key),
                Items = g.OrderBy(i => i.OrdenVisual).ToList()
            })
            .Where(g => g.Dia != null)
            .OrderBy(g => dayList.IndexOf(g.Dia!))
            .ToList();

        if (!groupedItems.Any())
        {
            container.PaddingTop(20).Text("No hay ejercicios asignados en la rutina.").Italic();
            return;
        }

        container.PaddingVertical(1, Unit.Centimetre).Column(column =>
        {
            column.Spacing(20);
            
            foreach (var group in groupedItems)
            {
                column.Item().EnsureSpace(110).Column(dayColumn =>
                {
                    dayColumn.Item().PaddingBottom(5).Text(group.Dia!.Nombre).FontSize(16).SemiBold().FontColor(Colors.Blue.Darken3);
                    
                    dayColumn.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(105);
                            columns.RelativeColumn();
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(70);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).AlignCenter().Text("Imagen");
                            header.Cell().Element(CellStyle).PaddingLeft(6).Text("Ejercicio");
                            header.Cell().Element(CellStyle).AlignCenter().Text("Series");
                            header.Cell().Element(CellStyle).AlignCenter().Text("Reps");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold().FontSize(11).FontColor(Colors.Blue.Darken3)).PaddingVertical(6).BorderBottom(1.5f).BorderColor(Colors.Blue.Darken2);
                            }
                        });

                        foreach (var item in group.Items)
                        {
                            if (item.EsEtiqueta)
                            {
                                table.Cell().ColumnSpan(4).ShowEntire().Element(CellStyle).Padding(7).Background(Colors.Blue.Lighten5).AlignCenter().Text(item.TextoEtiqueta).SemiBold().FontColor(Colors.Blue.Darken3);
                            }
                            else
                            {
                                var imageCell = table.Cell().ShowEntire().Element(CellStyle).Padding(3).Height(80).AlignCenter().AlignMiddle();
                                byte[]? imageBytes = ImageHelper.GetImageBytes(item.Ejercicio?.RutaImagen);

                                if (imageBytes != null && ImageHelper.IsValidImageBytes(imageBytes))
                                {
                                    imageCell.Image(imageBytes).FitArea();
                                }
                                else
                                {
                                    imageCell.Background(Colors.Blue.Lighten5).Text("Sin imagen").FontSize(8).FontColor(Colors.Blue.Darken1);
                                }

                                table.Cell().ShowEntire().Element(CellStyle).AlignMiddle().PaddingLeft(6).Column(col =>
                                {
                                    col.Item().Text(item.Ejercicio?.Nombre ?? "Desconocido").FontSize(12).SemiBold().FontColor(Colors.Blue.Darken4);
                                    if (!string.IsNullOrEmpty(item.Ejercicio?.GrupoMuscular))
                                    {
                                        col.Item().PaddingTop(2).Text(item.Ejercicio.GrupoMuscular).FontSize(9).FontColor(Colors.Blue.Darken2);
                                    }

                                    if (!string.IsNullOrWhiteSpace(item.Ejercicio?.UrlExplicacion))
                                    {
                                        var url = item.Ejercicio.UrlExplicacion.Trim();
                                        var navUrl = url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                                            ? url
                                            : $"https://{url}";

                                        col.Item().PaddingTop(3).Hyperlink(navUrl).Text(text =>
                                        {
                                            text.Span("[ Ver video / explicacion ]").FontSize(8.5f).FontColor(Colors.Blue.Darken2).SemiBold().Underline();
                                        });

                                        string displayUrl = url.Length > 42 ? url.Substring(0, 39) + "..." : url;
                                        col.Item().Text(displayUrl).FontSize(7.5f).FontColor(Colors.Grey.Darken1);
                                    }
                                });

                                table.Cell().ShowEntire().Element(CellStyle).AlignCenter().AlignMiddle().Text(item.Series.ToString()).FontSize(11);
                                table.Cell().ShowEntire().Element(CellStyle).AlignCenter().AlignMiddle().Text(item.Repeticiones.ToString()).FontSize(11);
                            }

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.BorderBottom(1).BorderColor(Colors.Blue.Lighten4).PaddingVertical(6);
                            }
                        }
                    });
                });
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(x =>
        {
            x.Span("Página ");
            x.CurrentPageNumber();
            x.Span(" de ");
            x.TotalPages();
        });
    }
}
#endif
