using Microsoft.Extensions.Logging;
using MySARAssist.Services;
using MySARAssist.ViewModels.CheckInOut;
using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;

namespace MySARAssist
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            // Add this code
            string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "mysarassist.db3");
            
            builder.Services.AddSingleton<PersonnelService>(s => ActivatorUtilities.CreateInstance<PersonnelService>(s, dbPath));

            builder.Logging.AddInMemoryLogger(options =>
            {
                
            });
            builder.Services.AddTransient<MainPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}