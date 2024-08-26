using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.Calculators;

public partial class VisualSearchResourceEstimationView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public VisualSearchResourceEstimationView(ILogger<MainPage> logger)
    {
        try
        {
            InitializeComponent();
            this.logger = logger;
            LogController.SuspendShake();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in VisualSearchResourceEstimationView constructor");

        }
    }

}