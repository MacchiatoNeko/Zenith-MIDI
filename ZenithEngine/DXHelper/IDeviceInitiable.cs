using System;

namespace ZenithEngine.DXHelper
{
    public interface IDeviceInitiable : IDisposable
    {
        public void Init(DeviceGroup device);
    }
}
