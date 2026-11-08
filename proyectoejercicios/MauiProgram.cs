using Microsoft.Extensions.Logging;
using proyectoejercicios.Data;

namespace proyectoejercicios
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddScoped<proyectoejercicios.Services.DragDropState>();
            builder.Services.AddScoped<proyectoejercicios.Services.RoutineBuilderState>();
            builder.Services.AddScoped<proyectoejercicios.Services.ThemeService>();
            builder.Services.AddScoped<proyectoejercicios.Application.Services.Pdf.PdfGeneratorService>();
#if WINDOWS || MACCATALYST
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DatabaseSeeder.Initialize(context);
            }

            return app;
        }
    }
}
