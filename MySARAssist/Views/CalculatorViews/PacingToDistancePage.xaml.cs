using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.Calculators;

public partial class PacingToDistancePage : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public PacingToDistancePage(ILogger<MainPage> logger)
	{
		InitializeComponent();
        LogController.SuspendShake();
        this.logger = logger;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

       

    }
}