using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.Calculators;

public partial class LinearSearchView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public LinearSearchView(ILogger<MainPage> logger)
	{
		InitializeComponent();
        LogController.SuspendShake();
        this.logger = logger;
    }
}