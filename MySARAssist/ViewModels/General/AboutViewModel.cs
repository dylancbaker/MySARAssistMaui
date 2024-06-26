using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySARAssist.ViewModels.General
{
    public class AboutViewModel : ObservableObject
    {
        public AboutViewModel()
        {
            VersionTracking.Track();
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/dylancbaker/MySARAssistMaui"));

        }
        public string VersionNumber => "Version " + VersionTracking.CurrentVersion;
        public ICommand OpenWebCommand { get; }
    }
}
