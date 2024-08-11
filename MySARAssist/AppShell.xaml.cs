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
            Routing.RegisterRoute("CheckInOut/EditPersonnel", typeof(PersonnelEditView));
            Routing.RegisterRoute("CheckInOut/EditPersonnel/Qualifications", typeof(EditQualificationsPage));

            Routing.RegisterRoute(nameof(RADeMSDetailsPage), typeof(RADeMSDetailsPage));

        }
    }
}
