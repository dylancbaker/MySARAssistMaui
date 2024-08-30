using Microsoft.Extensions.Logging;
using MySARAssist.Services;
using MySARAssist.ViewModels.CheckInOut;
using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using ZXing.Net.Maui.Controls;
using MySARAssist.Interfaces;
using MySARAssist.Views.Calculators;
using MySARAssist.Views;
using MySARAssist.Views.CheckInOut;
using MySARAssist.Views.RADeMS;
using Microsoft.Maui.LifecycleEvents;
using NewRelic.MAUI.Plugin;
using MySARAssist.Models;

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
                options.MaxLines = 1024;
                options.MinLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
                options.MaxLevel = Microsoft.Extensions.Logging.LogLevel.Critical;

            });
            builder.Logging.AddStreamingFileLogger(options =>
            {
                options.RetainDays = 2;
                options.FolderPath = Path.Combine(
                    FileSystem.CacheDirectory,
                    "MetroLogs");

            });
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AboutView>();

            builder.Services.AddTransient<CalculatorsView>();
            builder.Services.AddTransient<CoordinateConverterView>();
            builder.Services.AddTransient<DistanceToPacingPage>();
            builder.Services.AddTransient<GridSearchView>();
            builder.Services.AddTransient<HowToRangeOfDetectionPage>();
            builder.Services.AddTransient<LinearSearchView>();
            builder.Services.AddTransient<PacingToDistancePage>();
            builder.Services.AddTransient<SweepWidthCalculatorView>();
            builder.Services.AddTransient<VisualSearchResourceEstimationView>();

            builder.Services.AddTransient<CheckInOutView>();
            builder.Services.AddTransient<CheckInView>();
            builder.Services.AddTransient<CheckOutView>();
            builder.Services.AddTransient<EditQualificationsPage>();
            builder.Services.AddTransient<PersonnelEditView>();
            builder.Services.AddTransient<PersonnelListView>();

            builder.Services.AddTransient<RADeMSView>();
            builder.Services.AddTransient<RADeMSDetailsPage>();
            builder.Services.AddTransient<RADeMSCardPage>();

            EntryHandler.AddDone();
            builder.UseBarcodeReader();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.ConfigureLifecycleEvents(AppLifecycle => {
#if ANDROID
            AppLifecycle.AddAndroid(android => android
               .OnCreate((activity, savedInstanceState) => StartNewRelic()));
#endif
#if IOS

             AppLifecycle.AddiOS(iOS => iOS.WillFinishLaunching((_,__) => {
                StartNewRelic();
                return false;
            }));
#endif
            });

            return builder.Build();
        }

        private static void StartNewRelic()
        {

            CrossNewRelic.Current.HandleUncaughtException();

            // Set optional agent configuration
            // Options are: crashReportingEnabled, loggingEnabled, logLevel, collectorAddress, crashCollectorAddress, analyticsEventEnabled, networkErrorRequestEnabled, networkRequestEnabled, interactionTracingEnabled, webViewInstrumentation, fedRampEnabled, offlineStorageEnabled, newEventSystemEnabled, backgroundReportingEnabled
            // AgentStartConfiguration agentConfig = new AgentStartConfiguration(crashReportingEnabled:false);


            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                CrossNewRelic.Current.Start("AA32d4cf0465ae3269a836f3f40801a1bd04f4e60e-NRMA");
                // Start with optional agent configuration
                // CrossNewRelic.Current.Start("<APP-TOKEN-HERE>", agentConfig);
            }
            else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            {
                CrossNewRelic.Current.Start("AA6a713ecb46cfabf55e052bfd4f6297c55258ea9e-NRMA");
                // Start with optional agent configuration
                // CrossNewRelic.Current.Start("<APP-TOKEN-HERE>", agentConfig);
            }
        }
    }



}