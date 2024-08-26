using Android.Content.PM;
using MySARAssist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Platforms.Android
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        private static readonly IReadOnlyDictionary<DisplayOrientation, ScreenOrientation> _androidDisplayOrientationMap =
            new Dictionary<DisplayOrientation, ScreenOrientation>
            {
                [DisplayOrientation.Landscape] = ScreenOrientation.Landscape,
                [DisplayOrientation.Portrait] = ScreenOrientation.Portrait,
            };

        public void SetDeviceOrientation(DisplayOrientation displayOrientation)
        {
            var currentActivity = ActivityStateManager.Default.GetCurrentActivity();
            if (currentActivity is not null)
            {
                if (_androidDisplayOrientationMap.TryGetValue(displayOrientation, out ScreenOrientation screenOrientation))
                    currentActivity.RequestedOrientation = screenOrientation;
            }
        }
    }
}
