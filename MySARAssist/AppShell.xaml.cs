using MySARAssist.Views.CheckInOut;
using MySARAssist.Views.RADeMS;

namespace MySARAssist
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("CheckInOut", typeof(CheckInOutView));
            Routing.RegisterRoute(nameof(CheckInOutView)+"/" + nameof(PersonnelEditView), typeof(PersonnelEditView));
            Routing.RegisterRoute(nameof(CheckInOutView) + "/" + nameof(PersonnelEditView) + "/" + nameof(EditQualificationsPage), typeof(EditQualificationsPage));
            Routing.RegisterRoute(nameof(CheckInOutView) + "/" + nameof(PersonnelListView), typeof(PersonnelListView));

            Routing.RegisterRoute(nameof(RADeMSDetailsPage), typeof(RADeMSDetailsPage));

        }
    }
}
