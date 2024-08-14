



namespace MySARAssist.Views.CheckInOut;

[QueryProperty(nameof(PersonnelID), nameof(PersonnelID))]
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class PersonnelEditView : ContentPage
{
    ViewModels.CheckInOut.PersonnelEditViewModel _viewModel;
    public PersonnelEditView()
	{
		InitializeComponent();
        _viewModel = new ViewModels.CheckInOut.PersonnelEditViewModel();
        this.BindingContext = _viewModel;

    }

    public string PersonnelID
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            if (!string.IsNullOrEmpty(temp))
            {
                try { _ = _viewModel.SetTeamMember(new Guid(temp)); }
                catch { _ = _viewModel.SetTeamMember(Guid.NewGuid()); }
            }
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete member?", "Are you sure you want to delete this member?", "Yes", "No");
        if (answer)
        {
            _viewModel.DeleteCommand.Execute(null);
        }
    }

    private void pickParentOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        pickOrganization.ItemsSource = null;
        pickOrganization.ItemsSource = _viewModel.Organizations;

    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {

    }
}