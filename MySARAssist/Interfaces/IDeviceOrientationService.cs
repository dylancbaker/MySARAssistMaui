using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Interfaces
{
    public interface IDeviceOrientationService
    {
        void SetDeviceOrientation(DisplayOrientation displayOrientation);
    }
}
