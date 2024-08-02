namespace MySARAssist.Views.Calculators;

public partial class SweepWidthCalculatorView : ContentPage
{
	public SweepWidthCalculatorView()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        

    }

    private async void HowToRDImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Calculators.HowToRangeOfDetectionPage());
    }
}