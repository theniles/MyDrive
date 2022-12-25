using MyDrive.Maui.Services;

namespace MyDrive.Maui
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.RegisterDependencies().Build();
        }

        private static MauiAppBuilder RegisterDependencies(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DriveService>();

            return builder;
        }
    }
}