using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.Calculators;

public partial class HowToRangeOfDetectionPage : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public HowToRangeOfDetectionPage(ILogger<MainPage> logger)
	{
		InitializeComponent();
        LogController.SuspendShake();
        this.logger = logger;
    }
}