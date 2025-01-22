using MetroLog;
using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.CheckInOut;


public partial class CheckInOutView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public CheckInOutView(ILogger<MainPage> logger)
	{
        InitializeComponent();
        this.logger = logger;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LogController.ResumeShakeIfNeeded();
        if(this.BindingContext != null)
        {
            ViewModels.CheckInOut.CheckInManagementViewModel vm = (ViewModels.CheckInOut.CheckInManagementViewModel)this.BindingContext;
            vm.OnAppearing();

        }
    }

    private async void btnAddUser_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        await Shell.Current.GoToAsync("" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView));

    }

    private async void btnSignOut_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {

    }

    private async  void btnChangeSelectedMember_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("" + nameof(CheckInOutView) + "/" + nameof(PersonnelListView));
    }

    private async void btnEditMember_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        if (App.CurrentPerson == null)
        {
            await Shell.Current.GoToAsync("" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView));
        }
        else
        {
            await Shell.Current.GoToAsync("" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView) + $"PersonnelID={App.CurrentPerson.ID.ToString()}");

            //await Shell.Current.GoToAsync($"CheckInOut/EditPersonnel?PersonnelID={App.CurrentPerson.ID.ToString()}");
        }
    }

    private async void btnSelectUser_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("" + nameof(CheckInOutView) + "/" + nameof(PersonnelListView));

    }
}