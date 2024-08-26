

using Microsoft.Extensions.Logging;
using MySARAssist.ViewModels.RADeMS;
using MySarAssistModels.People;

namespace MySARAssist.Views.RADeMS;


[QueryProperty(nameof(OpRisk), nameof(OpRisk))]
[QueryProperty(nameof(ResCap), nameof(ResCap))]
public partial class RADeMSCardPage : ContentPage
{
    private readonly ILogger<MainPage> logger;
    RADeMSCardViewModel _viewModel = null;


    public RADeMSCardPage(ILogger<MainPage> logger)
    {
        try
        {
            InitializeComponent();
            this.logger = logger;
            this._viewModel = (RADeMSCardViewModel)BindingContext;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in RADeMSCardPage constructor");
        }
    }


    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        double h = GridImage.Height;
        double w = GridImage.Width;
        _viewModel.SetImageSize(w, h);
    }

    public string OpRisk
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            int i = 0;
            if (!string.IsNullOrEmpty(temp) && int.TryParse(temp, out i))
            {
                 _viewModel.SetOpRiskScore(i);
            }

            else
            {
                logger.LogWarning("OpRisk value is not a number");
            }
        }
    }


    public string ResCap
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            int i = 0;
            if (!string.IsNullOrEmpty(temp) && int.TryParse(temp, out i))
            {
                _viewModel.SetResponseCapacityScore(i);
            }

            else
            {
                logger.LogWarning("Response Capacity value is not a number");
            }
        }
    }
}
