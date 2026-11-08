using Microsoft.Extensions.Logging;
using proyectoejerciciosescritorio.Data;

namespace proyectoejerciciosescritorio
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

            builder.Services.AddScoped<proyectoejerciciosescritorio.Services.RoutineBuilderState>();
            builder.Services.AddScoped<proyectoejerciciosescritorio.Services.ThemeService>();
            builder.Services.AddScoped<proyectoejerciciosescritorio.Services.CommandKeyConfigService>();
            builder.Services.AddScoped<proyectoejerciciosescritorio.Application.Services.Pdf.PdfGeneratorService>();

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

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

