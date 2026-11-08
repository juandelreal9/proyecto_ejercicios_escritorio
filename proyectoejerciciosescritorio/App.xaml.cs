namespace proyectoejerciciosescritorio
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage())
            {
                Title = "Creador Rápido de Rutinas - Escritorio",
                Width = 1440,
                Height = 900,
                MinimumWidth = 1024,
                MinimumHeight = 680
            };
        }
    }
}

