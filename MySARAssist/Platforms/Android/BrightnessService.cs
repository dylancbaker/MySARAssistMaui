     // Platforms/Android/BrightnessService.android.cs
     using Android.App;
     using Android.Content;
     using Android.Provider;
     using Android.Views;

     namespace MySARAssist.Services
     {
         public partial class BrightnessService
         {
             public partial void SetBrightness(float brightness)
             {
                 var window = Platform.CurrentActivity.Window;
                 var layoutParams = window.Attributes;
                 layoutParams.ScreenBrightness = brightness;
                 window.Attributes = layoutParams;
             }
         }
     }
     