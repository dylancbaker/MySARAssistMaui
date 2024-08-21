     // Platforms/iOS/BrightnessService.ios.cs
     using UIKit;

     namespace MySARAssist.Services
     {
         public partial class BrightnessService
         {
             public partial void SetBrightness(float brightness)
             {
                 UIScreen.MainScreen.Brightness = brightness;
             }
         }
     }
     