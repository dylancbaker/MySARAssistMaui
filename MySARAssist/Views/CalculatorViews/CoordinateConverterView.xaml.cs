using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MySarAssistModels;
using MySARAssist.Models;
using MetroLog.Maui;
using Microsoft.Extensions.Logging;
namespace MySARAssist.Views.Calculators;

public partial class CoordinateConverterView : ContentPage
{
	public CoordinateConverterView(ILogger<MainPage> logger)
	{
		InitializeComponent();
        LogController.SuspendShake();
        this.logger = logger;
        cts = new CancellationTokenSource();
    }


    public async Task<PermissionStatus> CheckAndRequestLocationPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // Prompt the user to turn on in settings
            // On iOS once a permission has been denied it may not be requested again from the application
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            // Prompt the user with additional information as to why the permission is needed
        }

        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        return status;
    }


    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

       

    }
    private async void ShowToastAsync(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(message, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    private void btnGetCurrent_Clicked(object sender, EventArgs e)
    {
        setLocation();
    }

    #region Location
    private async void setLocation()
    {
        try
        {

            await CheckAndRequestLocationPermission();

            var locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null && location.Accuracy < 60)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                Coordinate c = new Coordinate();
                c.Latitude = location.Latitude;
                c.Longitude = location.Longitude;
                txtCoordinates.Text = c.DecimalDegrees;
                //DisplayQuestion();

                ShowToastAsync("Accuracy: " + location.Accuracy + "m");

            }
            else
            {
                //sendToast("The location was not accurate enough, please try again (" + location.Accuracy + "m)");
                await GetCurrentLocation();
            }
        }
        catch (FeatureNotSupportedException)
        {
            ShowToastAsync("Sorry, this feature is not supported on this device.");
            // Handle not supported on device exception
        }
        catch (FeatureNotEnabledException)
        {
            ShowToastAsync("Sorry, geolocation has not been enabled on this device. Check your device settings.");
            // Handle not enabled on device exception
        }
        catch (PermissionException)
        {
            ShowToastAsync("Sorry, you must enable permissions to get your current location.");
            // Handle permission exception
        }
        catch (Exception)
        {
            ShowToastAsync("Sorry, a problem occurred");
            // Unable to get location
        }
    }

    CancellationTokenSource cts;
    private readonly ILogger<MainPage> logger;

    async Task GetCurrentLocation()
    {
        try
        {
            var locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            if (location != null && location.Accuracy < 60)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                Coordinate c = new Coordinate();
                c.Latitude = location.Latitude;
                c.Longitude = location.Longitude;
                txtCoordinates.Text = c.DecimalDegrees;
                //DisplayQuestion();
                ShowToastAsync("Accuracy: " + location.Accuracy + "m");

            }
            else if (location != null)
            {
                ShowToastAsync("The location was not accurate enough (" + location.Accuracy + "m), please try again.");
            }
        }
        catch (FeatureNotSupportedException)
        {
            ShowToastAsync("Sorry, this feature is not supported on this device.");
            // Handle not supported on device exception
        }
        catch (FeatureNotEnabledException)
        {
            ShowToastAsync("Sorry, geolocation has not been enabled on this device. Check your device settings.");
            // Handle not enabled on device exception
        }
        catch (PermissionException)
        {
            ShowToastAsync("Sorry, you must enable permissions to get your current location.");
            // Handle permission exception
        }
        catch (Exception)
        {
            ShowToastAsync("Sorry, a problem occurred");
            // Unable to get location
        }
    }

    #endregion
}