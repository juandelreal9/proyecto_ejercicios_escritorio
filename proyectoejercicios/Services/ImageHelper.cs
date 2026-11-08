using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace proyectoejercicios.Services;

public static class ImageHelper
{
    public static string GetImageSrc(string? ruta)
    {
        if (string.IsNullOrWhiteSpace(ruta))
            return string.Empty;

        var trimmed = ruta.Trim();
        if (trimmed.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
        {
            return trimmed;
        }

        var normalized = trimmed.Replace(' ', '_');
        return $"images/{normalized}";
    }

    public static async Task<string?> PickImageAsBase64Async()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona una imagen para el ejercicio",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
                return null;

            using var stream = await result.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var bytes = ms.ToArray();

            string contentType = !string.IsNullOrWhiteSpace(result.ContentType) 
                ? result.ContentType 
                : GetContentTypeFromExtension(result.FileName);

            return $"data:{contentType};base64,{Convert.ToBase64String(bytes)}";
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static byte[]? GetImageBytes(string? ruta)
    {
        if (string.IsNullOrWhiteSpace(ruta))
            return null;

        var trimmed = ruta.Trim();
        var normalized = trimmed.Replace(' ', '_');

        try
        {
            // Base64 Data URL (e.g. data:image/png;base64,...)
            if (trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                int commaIndex = trimmed.IndexOf(',');
                if (commaIndex >= 0)
                {
                    string base64 = trimmed.Substring(commaIndex + 1).Trim();
                    return Convert.FromBase64String(base64);
                }
            }

            // Web URL
            if (trimmed.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
                var downloaded = client.GetByteArrayAsync(trimmed).GetAwaiter().GetResult();
                if (IsValidImageBytes(downloaded))
                    return downloaded;
            }

            // Local file path
            if (File.Exists(trimmed))
            {
                return File.ReadAllBytes(trimmed);
            }

            // In app directory or wwwroot
            string appDir = AppContext.BaseDirectory;
            string[] candidates =
            {
                Path.Combine(appDir, "wwwroot", "images", normalized),
                Path.Combine(appDir, "wwwroot", "images", trimmed),
                Path.Combine(appDir, "wwwroot", normalized),
                Path.Combine(appDir, "wwwroot", trimmed)
            };

            foreach (var candidate in candidates)
            {
                if (File.Exists(candidate))
                    return File.ReadAllBytes(candidate);
            }

            // Fallback for Android / iOS packaged MAUI assets
            string[] assetPaths =
            {
                $"wwwroot/images/{normalized}",
                $"wwwroot/images/{trimmed}",
                $"images/{normalized}",
                $"images/{trimmed}"
            };

            foreach (var assetPath in assetPaths)
            {
                try
                {
                    using var stream = FileSystem.OpenAppPackageFileAsync(assetPath).GetAwaiter().GetResult();
                    if (stream != null)
                    {
                        using var ms = new MemoryStream();
                        stream.CopyTo(ms);
                        var bytes = ms.ToArray();
                        if (IsValidImageBytes(bytes))
                            return bytes;
                    }
                }
                catch
                {
                    // Try next candidate
                }
            }
        }
        catch
        {
            // Ignore failure, return null to fallback safely
        }

        return null;
    }

    public static bool IsValidImageBytes(byte[]? bytes)
    {
        if (bytes == null || bytes.Length < 4)
            return false;

        // JPEG: FF D8 FF
        if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
            return true;

        // PNG: 89 50 4E 47
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return true;

        // GIF: 47 49 46 (GIF)
        if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46)
            return true;

        // WEBP: RIFF....WEBP
        if (bytes.Length >= 12 &&
            bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
            bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50)
            return true;

        // BMP: 42 4D (BM)
        if (bytes[0] == 0x42 && bytes[1] == 0x4D)
            return true;

        return false;
    }

    private static string GetContentTypeFromExtension(string fileName)
    {
        string ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            _ => "image/jpeg"
        };
    }
}
