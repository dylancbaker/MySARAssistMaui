namespace MySARAssist.Views.Calculators;

public partial class VisualSearchResourceEstimationView : ContentPage
{
	public VisualSearchResourceEstimationView()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > height)
        {
            slMainLayout.Orientation = StackOrientation.Horizontal;
        }
        else
        {
            slMainLayout.Orientation = StackOrientation.Vertical;
        }

    }
}