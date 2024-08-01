namespace MySARAssist.Views.CheckInOut;

public partial class PersonnelListView : ContentPage
{
	public PersonnelListView()
	{
		InitializeComponent();
	}

    private void lbMemberList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        lbMemberList.SelectedItem = null;



    }
}