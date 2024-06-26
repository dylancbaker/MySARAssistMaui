using MySARAssist.Models;

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
