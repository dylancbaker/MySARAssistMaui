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

}