﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.Calculators
{
    public class VisualSearchResourceEstimationViewModel : ObservableObject
    {
        public VisualSearchResourceEstimationViewModel()
        {



            SpeedUpCommand = new Command(() =>
            {
                Speed += 0.1;
                OnPropertyChanged(nameof(Speed));
            });
            SpeedDownCommand = new Command(() =>
            {
                if (Speed > 0.1) { Speed -= 0.1; }
                OnPropertyChanged(nameof(Speed));
            });

            CommandStaffUpCommand = new Command(() =>
            {
                CommandStaff += 1;
                OnPropertyChanged(nameof(CommandStaff));
            });
            CommandStaffDownCommand = new Command(() =>
            {
                if (CommandStaff > 0) { CommandStaff -= 1; }
                OnPropertyChanged(nameof(CommandStaff));
            });

            AreaUpCommand = new Command(() =>
            {
                Area += 0.1;
                OnPropertyChanged(nameof(Area));
            });
            AreaDownCommand = new Command(() =>
            {

                if (Area > 0.1) { Area -= 0.1; }
                OnPropertyChanged(nameof(Area));
            });

            SpacingUpCommand = new Command(() =>
            {
                Spacing += 1;
                OnPropertyChanged(nameof(Spacing));
            });
            SpacingDownCommand = new Command(() =>
            {
                if (Spacing > 1) { Spacing -= 1; }
                OnPropertyChanged(nameof(Spacing));
            });

            DurationUpCommand = new Command(() =>
            {
                Duration += 0.5;
                OnPropertyChanged(nameof(Duration));
            });
            DurationDownCommand = new Command(() =>
            {
                if (Duration > 0.5) { Duration -= 0.5; }
                OnPropertyChanged(nameof(Duration));
            });

            ExtraTravelTimeUpCommand = new Command(() =>
            {
                ExtraTravelTime += 0.1;
                OnPropertyChanged(nameof(ExtraTravelTime));
            });
            ExtraTravelTimeDownCommand = new Command(() =>
            {
                if (ExtraTravelTime > 0) { ExtraTravelTime -= 0.1; }
                OnPropertyChanged(nameof(ExtraTravelTime));
            });
        }

        private double _Area = 3.0;
        private double _Spacing = 10;
        private double _Speed = 1.6;
        private double _Duration = 6;
        private double _ExtraTravelTime = 0.25;
        private int _CommandStaff = 3;

        public double Area { get => Math.Round( _Area,2); set { _Area = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }
        public double Spacing { get => Math.Round(_Spacing, 1); set { _Spacing = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }
        public double Speed { get => Math.Round(_Speed, 1); set { _Speed = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }
        public double Duration { get => Math.Round(_Duration, 1); set { _Duration = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }
        public double ExtraTravelTime { get => Math.Round(_ExtraTravelTime, 2); set { _ExtraTravelTime = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }
        public int CommandStaff { get => _CommandStaff; set { _CommandStaff = value; OnPropertyChanged(nameof(ResourcesNeeded)); } }

        public int ResourcesNeeded
        {
            get { return CalculateResourcesNeeded(); }
        }

        private int CalculateResourcesNeeded()
        {

            double teamSize = 0;
            if (Duration > 0 && Speed > 0 && Spacing > 0)
            {
                double tempDuration = Duration + ExtraTravelTime * 2; //this will take the travel to and from assignments and account for it within the duration
                double tempArea = Area * 1000; //convert KMs to Meters to match the spacing measurment

                teamSize = tempArea / Spacing / Speed / tempDuration;

                //add in the command staff
                teamSize += CommandStaff;
                teamSize = Math.Ceiling(teamSize);
            }
            return (int)teamSize;



        }

        public Command SpeedUpCommand { get; }
        public Command SpeedDownCommand { get; }
        public Command DurationUpCommand { get; }
        public Command DurationDownCommand { get; }
        public Command AreaUpCommand { get; }
        public Command AreaDownCommand { get; }
        public Command SpacingUpCommand { get; }
        public Command SpacingDownCommand { get; }
        public Command CommandStaffUpCommand { get; }
        public Command CommandStaffDownCommand { get; }
        public Command ExtraTravelTimeUpCommand { get; }
        public Command ExtraTravelTimeDownCommand { get; }

    }
}
