namespace MySARAssist.Views.CheckInOut;

public partial class CheckInOutView : ContentPage
{
	public CheckInOutView()
	{
        InitializeComponent();
    }

    private async void btnAddUser_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());

    }

    private async void btnSignOut_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
    }

    private async  void btnChangeSelectedMember_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
    }

    private async void btnEditMember_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        if (App.CurrentPerson == null)
        {
            await Navigation.PushAsync(new Views.CheckInOut.PersonnelEditView());
        }
        else
        {
            await Shell.Current.GoToAsync($"CheckInOut/EditPersonnel?PersonnelID={App.CurrentPerson.ID.ToString()}");
        }
    }
}