using LibreHardwareMonitor.Hardware;
using System.Linq;

namespace SystemMonitoring.DevicePerformanceInfo
{
    public class WinDevicePerformanceInfo : IWinDevicePerformanceInfo
    {
        private readonly Computer _computer;
        private readonly IHardware _ram;
        private readonly IHardware _cpu;
        public WinDevicePerformanceInfo()
        {
            _computer = new Computer()
            {
                IsCpuEnabled = true,
                IsMemoryEnabled = true,
            };
            _computer.Open();
            _ram = _computer.Hardware.Where(x => x.HardwareType == HardwareType.Memory).FirstOrDefault();
            _cpu = _computer.Hardware.Where(x => x.HardwareType == HardwareType.Cpu).FirstOrDefault();
        }
        public DevicePerformance GetPerformanceInfo()
        {
            return new DevicePerformance()
            {
                CpuUsage = GetCpuUsage(),
                MemoryUsage = GetMemoryUsage(),
                CpuHeat = GetCpuTemperature(),
            };
        }

        private float GetMemoryUsage()
        {
            _ram.Update();
            var ramLoad = _ram.Sensors.Where(x => x.SensorType == SensorType.Load).FirstOrDefault();
            return ramLoad.Value.GetValueOrDefault();
        }

        private float GetCpuUsage()
        {
            _cpu.Update();
            var sensor = _cpu.Sensors
                .Where(x => x.SensorType == SensorType.Load && x.Name == "CPU Total").FirstOrDefault();
            return sensor.Value.GetValueOrDefault();
        }
        private float GetCpuTemperature()
        {
            _cpu.Update();
            var sensor = _cpu.Sensors
                .Where(x => x.SensorType == SensorType.Temperature && x.Name == "Core Average").FirstOrDefault();
            return sensor.Value.GetValueOrDefault();
        }
    }
}
