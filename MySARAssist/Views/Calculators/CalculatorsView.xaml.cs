namespace MySARAssist.Views;

public partial class CalculatorsView : ContentPage
{
	public CalculatorsView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }

    private async void CoordinateConverterButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.CoordinateConverterView());

    }
}