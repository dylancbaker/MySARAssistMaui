﻿using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.Calculators
{
    public class PacingCalculatorViewModel : ObservableObject
    {
       public PacingCalculatorViewModel()
        {
            _PacesPer100m = CurrentMemberPace;
            PaceUpCommand = new Command(() =>
            {
                PacesPer100m++;
                OnPropertyChanged(nameof(PacesPer100Text));

                updatePacing();

            });
            PaceDownCommand = new Command(() =>
            {
                PacesPer100m--;
                OnPropertyChanged(nameof(PacesPer100Text));

                updatePacing();

            });

            DistanceUpCommand = new Command(() =>
            {
                if ((currentMode??string.Empty).Equals("Distance"))
                {
                    PacesTaken++;
                    OnPropertyChanged(nameof(PacesTakenText));

                }
                else
                {
                    DistanceToTravel++;
                    OnPropertyChanged(nameof(DistanceToTravelText));

                }
                updatePacing();

            });
            DistanceDownCommand = new Command(() =>
            {
                if ((currentMode ?? string.Empty).Equals("Distance"))
                {
                    PacesTaken++;
                }
                else
                {
                    DistanceToTravel++;
                }
                updatePacing();

            });
            this._personnelService = new PersonnelService();
        }
        public double CurrentMemberPace
        {
            get
            {
                if (App.CurrentPerson != null) { return App.CurrentPerson.PacesPer100; }
                else { return 0; }
            }
            set { if (App.CurrentPerson != null) { App.CurrentPerson.PacesPer100 = value; updatePacing(); } }
        }
        private async void updatePacing()
        {

          
            if (App.CurrentPerson != null) { await _personnelService.UpdateItemAsync(App.CurrentPerson); }
        }


        private string? currentMode = null;

        double _PacesPer100m = 0;
        public double PacesPer100m
        {
            get => _PacesPer100m; set { _PacesPer100m = value; CurrentMemberPace = value; setResults(); OnPropertyChanged(nameof(PacesFromDistance)); OnPropertyChanged(nameof(DistanceFromPaces)); }
        }
        public string PacesPer100Text
        {
            get { if (PacesPer100m > 0) { return PacesPer100m.ToString(); } else { return ""; } }
            set { if (!string.IsNullOrEmpty(value)) { double.TryParse(value, out double temp); PacesPer100m = temp; } else { PacesPer100m = 0; } }
        }

        double _PacesTaken = 0;
        public double PacesTaken
        {
            get => _PacesTaken; set
            {
                _PacesTaken = value;
                currentMode = "Distance";
                setResults();
                OnPropertyChanged(nameof(PacesTaken)); OnPropertyChanged(nameof(DistanceFromPaces));
            }
        }
        public string PacesTakenText
        {
            get { if (PacesTaken > 0) { return PacesTaken.ToString(); } else { return ""; } }
            set { if (!string.IsNullOrEmpty(value)) { double.TryParse(value, out double temp); PacesTaken = temp; } else { PacesTaken = 0; } }
        }


        double _DistanceToTravel = 0;
        private readonly PersonnelService _personnelService;

        public double DistanceToTravel
        {
            get => _DistanceToTravel;
            set
            {
                _DistanceToTravel = value;
                currentMode = "Paces";
                setResults();
                OnPropertyChanged(nameof(DistanceToTravel)); OnPropertyChanged(nameof(PacesFromDistance));

            }
        }
        public string DistanceToTravelText
        {
            get { if (DistanceToTravel > 0) { return DistanceToTravel.ToString(); } else { return ""; } }
            set { if (!string.IsNullOrEmpty(value)) { double.TryParse(value, out double temp); DistanceToTravel = temp; } else { DistanceToTravel = 0; } }
        }

        private void setResults()
        {
            if (PacesPer100m != 0)
            {
                CurrentMemberPace = PacesPer100m;

                switch (currentMode)
                {
                    case "Paces":
                        CalculationResult = PacesFromDistance.ToString();
                        CalculationUnits = "paces";
                        CalculationTitle = "PACES REQUIRED";

                        break;
                    case "Distance":
                        CalculationResult = DistanceFromPaces.ToString();
                        CalculationUnits = "m";
                        CalculationTitle = "DISTANCE";
                        break;
                    default:
                        CalculationResult = "";
                        CalculationUnits = "";
                        CalculationTitle = "ENTER DETAILS ABOVE";
                        break;
                }

            }
            else
            {
                CalculationResult = "";
                CalculationUnits = "";
                CalculationTitle = "ENTER DETAILS ABOVE";
            }
            OnPropertyChanged(nameof(CalculationResult));
            OnPropertyChanged(nameof(CalculationUnits));
            OnPropertyChanged(nameof(CalculationTitle));
        }

        double PacesPerMeter
        {
            get { return PacesPer100m / 100; }
        }

        public double PacesFromDistance
        {
            get
            {
                double result = Math.Round(PacesPerMeter * DistanceToTravel, 1);
                return result;
            }
        }

        public double DistanceFromPaces
        {
            get
            {
                double result = Math.Round(PacesTaken / PacesPerMeter, 1);
                return result;
            }
        }

        public string CalculationResult { get; set; } = string.Empty;
        public string CalculationUnits { get; set; } = string.Empty;
        public string CalculationTitle { get; set; } = string.Empty;


        public Command PaceUpCommand { get; }
        public Command PaceDownCommand { get; }
        public Command DistanceUpCommand { get; }
        public Command DistanceDownCommand { get; }


    }


}
