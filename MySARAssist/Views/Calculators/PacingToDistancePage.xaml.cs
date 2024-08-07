namespace MySARAssist.Views.Calculators;

public partial class PacingToDistancePage : ContentPage
{
	public PacingToDistancePage()
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
}