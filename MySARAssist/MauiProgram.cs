using Microsoft.Extensions.Logging;
using MySARAssist.ViewModels;

namespace MySARAssist
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

            // Add this code
            string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "mysarassist.db3");
            builder.Services.AddSingleton<PersonnelListViewModel>(s => ActivatorUtilities.CreateInstance<PersonnelListViewModel>(s, dbPath));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
