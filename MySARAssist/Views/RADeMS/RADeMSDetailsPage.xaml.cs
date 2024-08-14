using MySARAssist.ViewModels.RADeMS;

namespace MySARAssist.Views.RADeMS;
[QueryProperty(nameof(RademsTypeIDStr), nameof(RademsTypeIDStr))]

public partial class RADeMSDetailsPage : ContentPage
{
    RADeMSDetailsViewModel _viewModel = null;

	public RADeMSDetailsPage()
	{
		InitializeComponent();

        _viewModel = new RADeMSDetailsViewModel();
        this.BindingContext = _viewModel;
	}


    public string RademsTypeIDStr
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            if (!string.IsNullOrEmpty(temp))
            {
                _viewModel.SetRademsType(temp);
            }
        }
    }

    public static Color GetColor(string baseColour, bool isSelected)
    {
        switch (baseColour)
        {
            case "Red":
                if (isSelected) { return Color.FromRgb(214, 40, 40); }
                else return Color.FromRgb(242, 132, 130);
            case "Green":
                if (isSelected) { return Color.FromRgb(14, 173, 105); }
                else return Color.FromRgb(132, 165, 157);
            case "Yellow":
                if (isSelected) { return Color.FromRgb(255, 159, 28); }
                else return Color.FromRgb(255, 229, 217);
            default:
                if (isSelected) { return Color.FromRgb(88, 88, 88); }
                else { return Color.FromRgb(151, 157, 172); }
        }
    }

}