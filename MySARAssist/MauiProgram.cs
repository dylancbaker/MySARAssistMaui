using Microsoft.Extensions.Logging;
using MySARAssist.Services;
using MySARAssist.ViewModels.CheckInOut;
using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using ZXing.Net.Maui.Controls;

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
            
            
            builder.Services.AddSingleton<PersonnelService>(s => ActivatorUtilities.CreateInstance<PersonnelService>(s));

            builder.Logging.AddInMemoryLogger(options =>
            {
                
            });
            builder.Services.AddTransient<MainPage>();
            builder.UseBarcodeReader();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}