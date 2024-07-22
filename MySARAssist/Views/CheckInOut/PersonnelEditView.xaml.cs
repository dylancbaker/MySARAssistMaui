

using MySARAssist.Models.People;

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
        _viewModel.TeamMemberID = Guid.NewGuid();
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