using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using proyectoejercicios.Models;
using proyectoejercicios.Services;
#if WINDOWS || MACCATALYST
using QuestPDF.Fluent;
using proyectoejercicios.Infrastructure.Pdf;
#elif ANDROID
using Android.Graphics;
using Android.Graphics.Pdf;
using Color = Android.Graphics.Color;
using Paint = Android.Graphics.Paint;
using Rect = Android.Graphics.Rect;
using RectF = Android.Graphics.RectF;
#endif

namespace proyectoejercicios.Application.Services.Pdf;

public class PdfGeneratorService
{
    public byte[] GenerateRoutinePdf(IEnumerable<ItemRutina> items, IEnumerable<DiaRutina> dias, string? titulo = null)
    {
        string finalTitle = string.IsNullOrWhiteSpace(titulo) ? "Mi Plan de Entrenamiento" : titulo.Trim();

#if WINDOWS || MACCATALYST
        var document = new RoutineDocument(items, dias, finalTitle);
        return document.GeneratePdf();
#elif ANDROID
        return GenerateAndroidNativePdf(items, dias, finalTitle);
#else
        return Array.Empty<byte>();
#endif
    }

#if ANDROID
    private static byte[] GenerateAndroidNativePdf(IEnumerable<ItemRutina> items, IEnumerable<DiaRutina> dias, string titulo)
    {
        const int pageWidth = 595;  // A4 width at 72 DPI
        const int pageHeight = 842; // A4 height at 72 DPI
        const float margin = 40f;
        const float contentWidth = pageWidth - (margin * 2);

        using var pdfDocument = new PdfDocument();

        var dayList = dias.ToList();
        var groupedItems = items
            .GroupBy(i => i.DiaId)
            .Select(g => new
            {
                Dia = dayList.FirstOrDefault(d => d.Id == g.Key),
                Items = g.OrderBy(i => i.OrdenVisual).ToList()
            })
            .Where(g => g.Dia != null)
            .OrderBy(g => dayList.IndexOf(g.Dia!))
            .ToList();

        using var titlePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#1D4ED8"), TextSize = 20f };
        titlePaint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

        using var subtitlePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#3B82F6"), TextSize = 10f };

        using var dayHeaderPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#1E3A8A"), TextSize = 14f };
        dayHeaderPaint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

        using var tableHeaderPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#1E3A8A"), TextSize = 10.5f };
        tableHeaderPaint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

        using var exNamePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#0F172A"), TextSize = 11f };
        exNamePaint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

        using var groupTagPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#0284C7"), TextSize = 9f };
        using var linkPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#2563EB"), TextSize = 8.5f };
        using var cellValuePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#1E293B"), TextSize = 10.5f, TextAlign = Paint.Align.Center };
        using var footerPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#64748B"), TextSize = 9.5f, TextAlign = Paint.Align.Center };

        using var labelBgPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#E0F2FE") };
        labelBgPaint.SetStyle(Paint.Style.Fill);
        using var labelBorderPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#38BDF8"), StrokeWidth = 1f };
        labelBorderPaint.SetStyle(Paint.Style.Stroke);
        using var labelTextPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#0C4A6E"), TextSize = 10.5f, TextAlign = Paint.Align.Center };
        labelTextPaint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

        using var headerLinePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#1D4ED8"), StrokeWidth = 1.5f };
        using var rowLinePaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#BFDBFE"), StrokeWidth = 1f };
        using var placeholderBgPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#EFF6FF") };
        placeholderBgPaint.SetStyle(Paint.Style.Fill);
        using var placeholderTextPaint = new Paint { AntiAlias = true, Color = Color.ParseColor("#60A5FA"), TextSize = 8f, TextAlign = Paint.Align.Center };

        int pageNumber = 1;
        PdfDocument.Page currentPage = StartNewPage(pdfDocument, pageWidth, pageHeight, pageNumber);
        Canvas canvas = currentPage.Canvas!;
        float y = margin;

        // Draw Main Document Header on Page 1
        canvas.DrawText(titulo, margin, y + 18f, titlePaint);
        y += 24f;
        canvas.DrawText($"Generado el: {DateTime.Now:dd/MM/yyyy}", margin, y + 10f, subtitlePaint);
        y += 28f;

        if (!groupedItems.Any())
        {
            canvas.DrawText("No hay ejercicios asignados en la rutina.", margin, y + 20f, subtitlePaint);
            DrawFooter(canvas, pageWidth, pageHeight, pageNumber, footerPaint);
            pdfDocument.FinishPage(currentPage);
        }
        else
        {
            const float colImgWidth = 75f;
            const float colSeriesWidth = 60f;
            const float colRepsWidth = 60f;
            float colNameWidth = contentWidth - colImgWidth - colSeriesWidth - colRepsWidth;

            float xImg = margin;
            float xName = xImg + colImgWidth;
            float xSeries = xName + colNameWidth;
            float xReps = xSeries + colSeriesWidth;

            foreach (var group in groupedItems)
            {
                if (y > pageHeight - 130f)
                {
                    DrawFooter(canvas, pageWidth, pageHeight, pageNumber, footerPaint);
                    pdfDocument.FinishPage(currentPage);
                    pageNumber++;
                    currentPage = StartNewPage(pdfDocument, pageWidth, pageHeight, pageNumber);
                    canvas = currentPage.Canvas!;
                    y = margin;
                }

                // Day Title
                canvas.DrawText(group.Dia!.Nombre, margin, y + 14f, dayHeaderPaint);
                y += 22f;

                // Table Header
                DrawTableHeader(canvas, margin, contentWidth, y, xImg, colImgWidth, xName, xSeries, colSeriesWidth, xReps, colRepsWidth, tableHeaderPaint, headerLinePaint);
                y += 22f;

                foreach (var item in group.Items)
                {
                    float rowHeight = item.EsEtiqueta ? 28f : 62f;

                    if (y + rowHeight > pageHeight - 55f)
                    {
                        DrawFooter(canvas, pageWidth, pageHeight, pageNumber, footerPaint);
                        pdfDocument.FinishPage(currentPage);
                        pageNumber++;
                        currentPage = StartNewPage(pdfDocument, pageWidth, pageHeight, pageNumber);
                        canvas = currentPage.Canvas!;
                        y = margin;

                        canvas.DrawText($"{group.Dia!.Nombre} (cont.)", margin, y + 14f, dayHeaderPaint);
                        y += 22f;
                        DrawTableHeader(canvas, margin, contentWidth, y, xImg, colImgWidth, xName, xSeries, colSeriesWidth, xReps, colRepsWidth, tableHeaderPaint, headerLinePaint);
                        y += 22f;
                    }

                    if (item.EsEtiqueta)
                    {
                        var rect = new RectF(margin, y + 2f, margin + contentWidth, y + rowHeight - 2f);
                        canvas.DrawRoundRect(rect, 6f, 6f, labelBgPaint);
                        canvas.DrawRoundRect(rect, 6f, 6f, labelBorderPaint);
                        string labelStr = string.IsNullOrWhiteSpace(item.TextoEtiqueta) ? "Nota / Indicación" : item.TextoEtiqueta;
                        canvas.DrawText(labelStr, margin + (contentWidth / 2f), y + 18f, labelTextPaint);
                        y += rowHeight;
                    }
                    else
                    {
                        // Image cell
                        var imgBox = new RectF(xImg + 6f, y + 5f, xImg + colImgWidth - 6f, y + rowHeight - 5f);
                        byte[]? imageBytes = ImageHelper.GetImageBytes(item.Ejercicio?.RutaImagen);
                        bool drewBitmap = false;

                        if (imageBytes != null && ImageHelper.IsValidImageBytes(imageBytes))
                        {
                            try
                            {
                                using var bmp = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                                if (bmp != null)
                                {
                                    canvas.DrawBitmap(bmp, null, imgBox, null);
                                    drewBitmap = true;
                                }
                            }
                            catch
                            {
                                drewBitmap = false;
                            }
                        }

                        if (!drewBitmap)
                        {
                            canvas.DrawRoundRect(imgBox, 6f, 6f, placeholderBgPaint);
                            canvas.DrawText("Sin img", imgBox.CenterX(), imgBox.CenterY() + 3f, placeholderTextPaint);
                        }

                        // Exercise Name & Info
                        string nombre = item.Ejercicio?.Nombre ?? "Ejercicio";
                        if (nombre.Length > 38) nombre = nombre.Substring(0, 35) + "...";
                        canvas.DrawText(nombre, xName + 8f, y + 22f, exNamePaint);

                        if (!string.IsNullOrWhiteSpace(item.Ejercicio?.GrupoMuscular))
                        {
                            canvas.DrawText(item.Ejercicio.GrupoMuscular, xName + 8f, y + 36f, groupTagPaint);
                        }

                        if (!string.IsNullOrWhiteSpace(item.Ejercicio?.UrlExplicacion))
                        {
                            string urlText = item.Ejercicio.UrlExplicacion.Trim();
                            if (urlText.Length > 42) urlText = urlText.Substring(0, 39) + "...";
                            canvas.DrawText($"Video: {urlText}", xName + 8f, y + 49f, linkPaint);
                        }

                        // Series & Reps
                        canvas.DrawText(item.Series.ToString(), xSeries + (colSeriesWidth / 2f), y + (rowHeight / 2f) + 4f, cellValuePaint);
                        canvas.DrawText(item.Repeticiones.ToString(), xReps + (colRepsWidth / 2f), y + (rowHeight / 2f) + 4f, cellValuePaint);

                        y += rowHeight;
                        canvas.DrawLine(margin, y, margin + contentWidth, y, rowLinePaint);
                    }
                }

                y += 18f;
            }

            DrawFooter(canvas, pageWidth, pageHeight, pageNumber, footerPaint);
            pdfDocument.FinishPage(currentPage);
        }

        using var ms = new MemoryStream();
        pdfDocument.WriteTo(ms);
        return ms.ToArray();
    }

    private static PdfDocument.Page StartNewPage(PdfDocument doc, int width, int height, int pageNum)
    {
        var pageInfo = new PdfDocument.PageInfo.Builder(width, height, pageNum).Create();
        return doc.StartPage(pageInfo)!;
    }

    private static void DrawTableHeader(
        Canvas canvas,
        float margin,
        float contentWidth,
        float y,
        float xImg,
        float colImgWidth,
        float xName,
        float xSeries,
        float colSeriesWidth,
        float xReps,
        float colRepsWidth,
        Paint headerPaint,
        Paint linePaint)
    {
        canvas.DrawText("Imagen", xImg + 12f, y + 14f, headerPaint);
        canvas.DrawText("Ejercicio", xName + 8f, y + 14f, headerPaint);
        canvas.DrawText("Series", xSeries + 14f, y + 14f, headerPaint);
        canvas.DrawText("Reps", xReps + 16f, y + 14f, headerPaint);
        canvas.DrawLine(margin, y + 19f, margin + contentWidth, y + 19f, linePaint);
    }

    private static void DrawFooter(Canvas canvas, int pageWidth, int pageHeight, int pageNum, Paint footerPaint)
    {
        canvas.DrawText($"Página {pageNum}", pageWidth / 2f, pageHeight - 20f, footerPaint);
    }
#endif
}
