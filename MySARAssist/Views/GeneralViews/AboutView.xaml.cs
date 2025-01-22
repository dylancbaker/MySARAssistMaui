using Microsoft.Extensions.Logging;

namespace MySARAssist.Views;

public partial class AboutView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public AboutView(ILogger<MainPage> logger)
	{
		InitializeComponent();
        VersionTracking.Track();
        this.logger = logger;
    }
}