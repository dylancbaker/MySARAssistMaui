namespace MySARAssist.Views.CheckInOut;

public partial class PersonnelListView : ContentPage
{
	public PersonnelListView()
	{
		InitializeComponent();
	}

    private void lbMemberList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        



    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext != null)
        {
            ViewModels.CheckInOut.PersonnelListViewModel vm = (ViewModels.CheckInOut.PersonnelListViewModel)this.BindingContext;
            vm.OnAppearing();

        }
    }
}