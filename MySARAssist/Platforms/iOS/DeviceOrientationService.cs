using Foundation;
using MySARAssist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MySARAssist.Platforms.iOS
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        private readonly IReadOnlyDictionary<DisplayOrientation, UIInterfaceOrientation> _iosDisplayOrientationMap =
           new Dictionary<DisplayOrientation, UIInterfaceOrientation>
           {
               [DisplayOrientation.Landscape] = UIInterfaceOrientation.LandscapeLeft,
               [DisplayOrientation.Portrait] = UIInterfaceOrientation.Portrait,
           };

        public void SetDeviceOrientation(DisplayOrientation displayOrientation)
        {
            OrientationManager.LockOrientation = true;
            if (displayOrientation == DisplayOrientation.Portrait)
            {
                OrientationManager.CurrentOrientation = UIInterfaceOrientationMask.Portrait;
            }
            else
            {
                OrientationManager.CurrentOrientation = UIInterfaceOrientationMask.Landscape;
            }

            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)_iosDisplayOrientationMap[displayOrientation]), new NSString("orientation"));
        }
    }

    public static class OrientationManager
    {
        public static bool LockOrientation { get; set; }
        public static UIInterfaceOrientationMask CurrentOrientation { get; set; } = UIInterfaceOrientationMask.All;
    }
}
