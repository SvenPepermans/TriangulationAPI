using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.DTOs;
using TriangulationAPI.Models;

namespace TriangulationAPI.Services
{
    public interface IDeviceServices
    {
        Task<Device> AddDevice(DeviceDTO device);
        Task CalculatePosition(Device device);
        Task<IEnumerable<Device>> GetAllDevices();
        Task<Device> UpdateDevice(DeviceDTO device, Device existingDevice);
        Task<Device> GetDeviceByMAC(string mac);
    }
}
