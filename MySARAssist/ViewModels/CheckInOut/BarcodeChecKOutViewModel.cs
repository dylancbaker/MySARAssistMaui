using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class BarcodeChecKOutViewModel : ObservableObject
    {
        public BarcodeChecKOutViewModel()
        {
            SignOutTime = DateTime.Now.TimeOfDay;
            KMs = 0;

        }

        private TimeSpan _SignOutTime;
        private int _KMs;

        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentPerson == null) { return "-No member selected-"; }
                return App.CurrentPerson.NameWithGroup ?? "-No member selected-";
            }
        }

        public TimeSpan SignOutTime
        {
            get => _SignOutTime; set
            {
                _SignOutTime = value;
                OnPropertyChanged(nameof(SignOutTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }
        public int KMs
        {
            get => _KMs;
            set
            {
                _KMs = value;
                OnPropertyChanged(nameof(KMs));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public string FullQRString
        {
            get
            {
                if(App.CurrentPerson == null) { return string.Empty; }
                try
                {

                    string qrString = "^" + App.CurrentPerson.StringForQRV6();
                    qrString += convertTimespanToDate(SignOutTime).ToString("HH:mm:ss") + ";";
                    qrString += KMs + ";";
                    qrString += "^";
                    return qrString;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

       
        private DateTime convertTimespanToDate(TimeSpan ts)
        {
            DateTime today = DateTime.Now;

            DateTime dt = new DateTime(today.Year, today.Month, today.Day);
            dt = dt + ts;
            return dt;
        }
    }
}
