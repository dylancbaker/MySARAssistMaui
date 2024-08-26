﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using MySarAssistModels;
using MySARAssist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MySARAssist.ViewModels.Calculators
{
    public class CoordinateConverterViewModel : ObservableObject
    {
        private Coordinate _coordinate = new Coordinate();
        public Command GetLocationCommand { get; }
        public Command CopyUTMCommand { get; }
        public Command CopyShortUTMCommand { get; }
        public Command CopyDDCommand { get; }
        public Command CopyDMSCommand { get; }
        public Command CopyMGRSCommand { get; }
        public Command OpenMapCommand { get; }

        public CoordinateConverterViewModel()
        {
            GetLocationCommand = new Command(OnGetLocationCommand);
            CopyUTMCommand = new Command(async () => await OnCopyUTMCommandAsync());
            CopyShortUTMCommand = new Command(async () => await OnCopyShortUTMCommandAsync());
            CopyDDCommand = new Command(async () => await OnCopyDDCommandAsync());
            CopyDMSCommand = new Command(async () => await OnCopyDMSCommandAsync());
            CopyMGRSCommand = new Command(async () => await OnCopyMGRSCommandAsync());
            OpenMapCommand = new Command(async () => await OnOpenMapCommandAsync());
        }


        private void OnGetLocationCommand()
        {
        }

        private async void ShowToastAsync(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        private async Task OnCopyUTMCommandAsync() { await Clipboard.SetTextAsync(UTM);  ShowToastAsync("Copied!"); }
        private async Task OnCopyShortUTMCommandAsync() { await Clipboard.SetTextAsync(ShortUTM); ShowToastAsync("Copied!"); }
        private async Task OnCopyDDCommandAsync() { await Clipboard.SetTextAsync(DecimalDegrees); ShowToastAsync("Copied!"); }
        private async Task OnCopyDMSCommandAsync() { await Clipboard.SetTextAsync(DMS); ShowToastAsync("Copied!"); }
        private async Task OnCopyMGRSCommandAsync() { await Clipboard.SetTextAsync(MGRS); ShowToastAsync("Copied!"); }
        private async Task OnOpenMapCommandAsync()
        {
            if (!string.IsNullOrEmpty(DecimalDegrees))
            {
                DevicePlatform devicePlatform = DeviceInfo.Current.Platform;
                if (devicePlatform == DevicePlatform.iOS)
                {
                    await Launcher.OpenAsync("http://maps.apple.com/?q=" + _coordinate.DecimalDegreesForURL);
                }
                else if (devicePlatform == DevicePlatform.Android)
                {
                    await Launcher.OpenAsync("geo:0,0?q=" + _coordinate.DecimalDegreesForURL);


                }
                else if (devicePlatform == DevicePlatform.WinUI)
                {
                    await Launcher.OpenAsync("bingmaps:?where=" + _coordinate.DecimalDegreesForURL);

                }
            }
        }

        private void TrySettingCoordinate()
        {
            if (string.IsNullOrEmpty(CoordinateInputText)) { CoordinatesOk = false; _coordinate = new Coordinate(); }
            else if (_coordinate.TryParseCoordinate(CoordinateInputText, out _coordinate))
            {
                CoordinatesOk = true;
            }
            else
            {
                CoordinatesOk = false; _coordinate = new Coordinate();
            }
            OnPropertyChanged(nameof(CoordinateParseResult));
            RefreshCoordinates();


        }

        private string _CoordinateInputText = "";
        public string CoordinateInputText
        {
            get { return _CoordinateInputText; }
            set { _CoordinateInputText = value; TrySettingCoordinate(); }
        }

        private bool CoordinatesOk;
        public string CoordinateParseResult
        {
            get
            {
                if (CoordinatesOk) { return "Coordinates OK"; }
                else { return "Coordinates not understood"; }

            }
        }

        public string UTM { get { if (CoordinatesOk) { return _coordinate.UTM; } else { return string.Empty; } } }
        public string MGRS { get { if (CoordinatesOk) { return _coordinate.MGRS; } else { return string.Empty; } } }
        public string DecimalDegrees { get { if (CoordinatesOk) { return _coordinate.DecimalDegrees; } else { return string.Empty; } } }
        public string DMS { get { if (CoordinatesOk) { return _coordinate.DegreesMinutesSeconds; } else { return string.Empty; } } }
        public string DMSLatitude { get { if (CoordinatesOk) { return _coordinate.DMSLatitude; } else { return string.Empty; } } }
        public string DMSLongitude { get { if (CoordinatesOk) { return _coordinate.DMSLongitude; } else { return string.Empty; } } }
        public string ShortUTM { get { if (CoordinatesOk) { return _coordinate.ShortUTM; } else { return string.Empty; } } }

        private void RefreshCoordinates()
        {
            OnPropertyChanged(nameof(UTM));
            OnPropertyChanged(nameof(MGRS));
            OnPropertyChanged(nameof(DecimalDegrees));
            OnPropertyChanged(nameof(DMS));
            OnPropertyChanged(nameof(ShortUTM));
            OnPropertyChanged(nameof(DMSLatitude));
            OnPropertyChanged(nameof(DMSLongitude));
        }
    }
}
