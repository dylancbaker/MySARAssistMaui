using Microsoft.Extensions.Logging;

namespace MySARAssist.Views;

public partial class RADeMSView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public RADeMSView(ILogger<MainPage> logger)
	{
		InitializeComponent();
        this.logger = logger;
    }
}