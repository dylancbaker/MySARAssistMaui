/* Unmerged change from project 'MySARAssist (net8.0-ios)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-android)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-windows10.0.19041.0)'
Added:
using MySARAssist.Models.Personnel;
*/
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel.Personnel;

namespace MySARAssist
{
    public partial class App : Application
    {
        public static Personnel? CurrentPerson { get; set; }


        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
