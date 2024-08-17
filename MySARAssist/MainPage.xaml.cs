using MySARAssist.Views.CheckInOut;

namespace MySARAssist
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void CalculatorsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CalculatorsView());

        }

     
        private async void CheckInOutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//" + nameof(CheckInOutView));
        }

        private async void RADeMSButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.RADeMSView());

        }
    }

}
