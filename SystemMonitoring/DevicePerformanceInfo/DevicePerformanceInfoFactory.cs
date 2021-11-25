using System;
using System.Runtime.InteropServices;

namespace SystemMonitoring.DevicePerformanceInfo
{
    public class DevicePerformanceInfoFactory
    {
        
        public static IDevicePerformanceInfo Create()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new MacDevicePerformanceInfo();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new PiDevicePerformanceInfo();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WinDevicePerformanceInfo();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
