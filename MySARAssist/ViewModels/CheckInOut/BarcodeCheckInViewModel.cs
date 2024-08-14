using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySarAssistModels.People;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class BarcodeCheckInViewModel : ObservableObject
    {
        public BarcodeCheckInViewModel()
        {
            SignInTime = DateTime.Now.TimeOfDay;
            MustBeOutTime = DateTime.Now.TimeOfDay;
        }

        private TimeSpan _SignInTime;
        private TimeSpan _MustBeOutTime;
        private bool _UseMustBeOutTime;

        public string? CurrentMemberName
        {
            get
            {
                if (App.CurrentPerson != null) { return App.CurrentPerson.NameWithGroup; }
                else { return "-No member selected-"; }
            }
        }

        public TimeSpan SignInTime
        {
            get => _SignInTime; set
            {
                _SignInTime = value;
                OnPropertyChanged(nameof(SignInTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }

        public TimeSpan MustBeOutTime
        {
            get => _MustBeOutTime; set
            {
                _MustBeOutTime = value;
                OnPropertyChanged(nameof(MustBeOutTime));
                OnPropertyChanged(nameof(FullQRString));
            }
        }
        public bool UseMustBeOutTime
        {
            get => _UseMustBeOutTime;
            set
            {
                _UseMustBeOutTime = value;
                OnPropertyChanged(nameof(UseMustBeOutTime));
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
                    string qrString = "~" + App.CurrentPerson.StringForQR();
                    qrString += convertTimespanToDate(SignInTime).ToString("HH:mm:ss") + ";";
                    if (UseMustBeOutTime)
                    {
                        qrString += convertTimespanToDate(MustBeOutTime).ToString("HH:mm:ss") + ";";
                    }
                    qrString += "~";



                    return qrString;
                }
                catch { return string.Empty; }
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
