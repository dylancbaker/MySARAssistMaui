using MetroLog;
using Microsoft.Extensions.Logging;
using MySARAssist.Views;
using MySARAssist.Views.CheckInOut;

namespace MySARAssist
{
    public partial class MainPage : ContentPage
    {
        private readonly ILogger<MainPage> _logger;

        public MainPage(ILogger<MainPage> logger)
        {
            InitializeComponent();
            this._logger = logger;
        }

        private async void CalculatorsButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.CalculatorsView());
            await Shell.Current.GoToAsync("//" + nameof(CalculatorsView));
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, "Moving to the calculator page");
        }


        private async void CheckInOutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//" + nameof(CheckInOutView));
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, "Moving to the check in page");

        }

        private async void RADeMSButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.RADeMSView());
            await Shell.Current.GoToAsync("//" + nameof(RADeMSView));
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, "Moving to the radems page");



        }
    }

}
