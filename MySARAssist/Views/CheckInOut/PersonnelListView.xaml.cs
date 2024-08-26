using Microsoft.Extensions.Logging;

namespace MySARAssist.Views.CheckInOut;

public partial class PersonnelListView : ContentPage
{
    private readonly ILogger<MainPage> logger;

    public PersonnelListView(ILogger<MainPage> logger)
	{
		InitializeComponent();
        this.logger = logger;
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