namespace MySARAssist.Views.CheckInOut;

public partial class CheckInOutView : ContentPage
{
	public CheckInOutView()
	{
        InitializeComponent();
    }

    private async void btnAddUser_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        await Shell.Current.GoToAsync("//" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView));

    }

    private async void btnSignOut_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {

    }

    private async  void btnChangeSelectedMember_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//" + nameof(CheckInOutView) + "/" + nameof(PersonnelListView));
    }

    private async void btnEditMember_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        if (App.CurrentPerson == null)
        {
            await Shell.Current.GoToAsync("//" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView));
        }
        else
        {
            await Shell.Current.GoToAsync("//" + nameof(CheckInOutView) + "/" + nameof(PersonnelEditView) + $"PersonnelID={App.CurrentPerson.ID.ToString()}");

            //await Shell.Current.GoToAsync($"CheckInOut/EditPersonnel?PersonnelID={App.CurrentPerson.ID.ToString()}");
        }
    }

    private async void btnSelectUser_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//" + nameof(CheckInOutView) + "/" + nameof(PersonnelListView));

    }
}