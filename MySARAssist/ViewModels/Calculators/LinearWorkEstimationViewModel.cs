using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.Calculators
{
    public class LinearWorkEstimationViewModel : ObservableObject
    {
        public LinearWorkEstimationViewModel()
        {
            CalculateCommand = new Command(() =>
            {
                CalculateEstimate();

            });

            EraseCommand = new Command(() =>
            {
                Length = 0;
                SearcherSpeed = 1.6;
                estimatedDuration = 0;
            });

            SpeedUpCommand = new Command(() =>
            {
                SearcherSpeed += 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));

            });
            SpeedDownCommand = new Command(() =>
            {
                SearcherSpeed -= 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));
            });


            LengthUpCommand = new Command(() =>
            {
                Length += 0.1;
                OnPropertyChanged(nameof(Length));
            });
            LengthDownCommand = new Command(() =>
            {
                Length -= 0.1;
                OnPropertyChanged(nameof(Length));
            });

            ElevationUpCommand = new Command(() =>
            {
                Elevation += 100;
                OnPropertyChanged(nameof(Elevation));
            });
            ElevationDownCommand = new Command(() =>
            {
                Elevation -= 100;
                OnPropertyChanged(nameof(Elevation));
            });

        }

        public Command CalculateCommand { get; }
        public Command EraseCommand { get; }
        public Command SpeedUpCommand { get; }
        public Command SpeedDownCommand { get; }
        public Command LengthUpCommand { get; }
        public Command LengthDownCommand { get; }
        public Command ElevationUpCommand { get; }
        public Command ElevationDownCommand { get; }


        private void CalculateEstimate()
        {
            if (Length > 0 && SearcherSpeed > 0)
            {

                estimatedDuration = (Length + (5.3 * (Elevation/1000))) / SearcherSpeed;
                estimatedDurationWithRoundTrip = ((Length + (5.3 * (Elevation / 1000))) / SearcherSpeed) + ((Length + (5.3)) / SearcherSpeed);
            }
            else { estimatedDuration = 0; }

            OnPropertyChanged(nameof(EstimatedDuration));
            OnPropertyChanged(nameof(EstimatedDurationWithRoundTrip));
        }

        double estimatedDuration = 0;
        double estimatedDurationWithRoundTrip = 0;
        public string EstimatedDuration
        {
            get => string.Format("{0:#,##0.0}", estimatedDuration);
        }
        public string EstimatedDurationWithRoundTrip
        {
            get => string.Format("{0:#,##0.0}", estimatedDurationWithRoundTrip);
        }


        double _searcherSpeed = 1.6;
        public double SearcherSpeed { get => _searcherSpeed; set { _searcherSpeed = value; CalculateEstimate(); OnPropertyChanged(nameof(SearcherSpeed)); OnPropertyChanged(nameof(SearcherSpeedStr)); } }
        public string? SearcherSpeedStr
        {
            get { if (SearcherSpeed > 0) { return Math.Round( SearcherSpeed,2).ToString(); } else { return null; } }
            set { if (!string.IsNullOrEmpty(value)) { double temp; double.TryParse(value, out temp); SearcherSpeed = temp; } }
        }
        double _length = 0;
        public double Length { get => _length; set { _length = value; CalculateEstimate(); OnPropertyChanged(nameof(Length)); OnPropertyChanged(nameof(LengthStr)); } }
        public string? LengthStr
        {
            get { if (Length > 0) { return Math.Round( Length,1).ToString(); } else { return null; } }
            set { if (!string.IsNullOrEmpty(value)) { double temp; double.TryParse(value, out temp); Length = temp; } }
        }

        double _elevation = 0;
        public double Elevation { get => _elevation; set { _elevation = value; CalculateEstimate(); OnPropertyChanged(nameof(Elevation)); OnPropertyChanged(nameof(ElevationStr)); } }
        public string? ElevationStr
        {
            get { if (Elevation > 0) { return Math.Round(Elevation, 1).ToString(); } else { return null; } }
            set { if (!string.IsNullOrEmpty(value)) { double temp; double.TryParse(value, out temp); Elevation = temp; } }
        }
    }
}
