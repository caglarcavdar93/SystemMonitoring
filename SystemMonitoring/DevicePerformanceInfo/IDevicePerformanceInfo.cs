using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitoring.DevicePerformanceInfo
{
    public interface IDevicePerformanceInfo
    {
        public DevicePerformance GetPerformanceInfo();
    }
}
