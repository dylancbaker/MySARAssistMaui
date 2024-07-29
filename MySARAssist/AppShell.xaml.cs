using MySARAssist.Views.CheckInOut;

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
        }
    }
}
