using Microsoft.Extensions.Logging;
using MySARAssist.Services;

namespace MySARAssist.Views.CheckInOut;

public partial class CheckOutView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public CheckOutView(ILogger<MainPage> logger)
	{
        try
        {
            this.logger = logger;
            InitializeComponent();
            


        }catch (Exception ex)
        {
            logger.LogError(ex, "Error in CheckOutView constructor");
        }
    }
}