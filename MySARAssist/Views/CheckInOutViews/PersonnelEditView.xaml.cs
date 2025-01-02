



using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.CheckInOut;

[QueryProperty(nameof(PersonnelID), nameof(PersonnelID))]
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class PersonnelEditView : ContentPage
{
    ViewModels.CheckInOut.PersonnelEditViewModel _viewModel;
    private readonly ILogger<MainPage> logger;

    public PersonnelEditView(ILogger<MainPage> logger)
	{ try
        {
            InitializeComponent();
            _viewModel = new ViewModels.CheckInOut.PersonnelEditViewModel();
            this.BindingContext = _viewModel;
            this.logger = logger;
        }catch (Exception ex)
        {
            logger.LogError(ex, "Error in PersonnelEditView constructor");
        }
    }

    public string PersonnelID
    {
        set
        {
            string temp = Uri.UnescapeDataString(value ?? string.Empty);
            if (!string.IsNullOrEmpty(temp))
            {
                try { _ = _viewModel.SetTeamMember(new Guid(temp)); }
                catch (Exception ex) { _ = _viewModel.SetTeamMember(Guid.NewGuid());
                logger.LogError(ex, "Error in PersonnelEditView.PersonnelID.set");
                }
            }
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete member?", "Are you sure you want to delete this member?", "Yes", "No");
        if (answer)
        {
            _viewModel.DeleteCommand.Execute(null);
            logger.LogInformation("Member deleted");
        }
    }

    private void pickParentOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pickOrganization.ItemsSource = null;
        //pickOrganization.ItemsSource = _viewModel.Organizations;

    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {

    }
}