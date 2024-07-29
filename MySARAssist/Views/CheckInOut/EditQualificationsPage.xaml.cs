namespace MySARAssist.Views.CheckInOut;

[QueryProperty(nameof(PersonnelID), nameof(PersonnelID))]
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class EditQualificationsPage : ContentPage
{
    ViewModels.CheckInOut.EditQualificationsViewModel _viewModel;

    public EditQualificationsPage()
	{
		InitializeComponent();
        _viewModel = new ViewModels.CheckInOut.EditQualificationsViewModel();
        this.BindingContext = _viewModel;

    }

    public string PersonnelID
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            if (!string.IsNullOrEmpty(temp))
            {
                try { _viewModel.TeamMemberID = new Guid(temp); }
                catch { _viewModel.TeamMemberID = Guid.NewGuid(); }
            }
        }
    }

}