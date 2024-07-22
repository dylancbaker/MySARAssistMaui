namespace MySARAssist.Views;

public partial class CalculatorsView : ContentPage
{
	public CalculatorsView()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > height)
        {
            slPageContent.Orientation = StackOrientation.Horizontal;
        }
        else
        {
            slPageContent.Orientation = StackOrientation.Vertical;
        }

    }

    private async void CoordinateConverterButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.CoordinateConverterView());

    }


    private async void GridSearch_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.GridSearchView());
    }

    private async void LinearSearch_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.LinearSearchView());
    }

    private async void VisualSearchResources_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.VisualSearchResourceEstimationView());

    }

    private async void Pacing_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.PacingToDistancePage());

    }

    private async void VisualSearch_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.SweepWidthCalculatorView());

    }


    private async void DistToPacing_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.DistanceToPacingPage());

    }
}