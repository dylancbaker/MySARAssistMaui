using MySARAssist.Services;

namespace MySARAssist.Views.CheckInOut;

public partial class CheckInView : ContentPage
{
    float _lastBrightness = 0.5f;

    public CheckInView()
	{
		InitializeComponent();

        // Example usage in your MAUI application
        var brightnessService = new BrightnessService();
        
        brightnessService.SetBrightness(1.0f); // Set brightness to maximum


        
        // (Optional) Later on reset the brightness to what it was
        //brightnessService.SetBrightness(previousBrightness);

    }

  
}