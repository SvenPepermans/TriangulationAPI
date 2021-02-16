using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.Data;
using TriangulationAPI.DTOs;
using TriangulationAPI.Models;

namespace TriangulationAPI.Services
{
    public class DeviceServices : IDeviceServices
    {
        private readonly ApplicationDbContext context;

        public DeviceServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Device> AddDevice(DeviceDTO device)
        {
            var deviceToAdd = new Device()
            {
                MACAdress = device.MACAdress,                
                DistanceA = device.DistanceA,
                DistanceB = device.DistanceB              
            };

            await context.AddAsync(deviceToAdd);
            await context.SaveChangesAsync();
            return deviceToAdd;
        }

        public Task CalculatePosition(Device device)
        {
            return context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Device>> GetAllDevices()
        {
            return await context.Devices.ToListAsync();
        }

         public Task<Device> GetDeviceByMAC(string mac)
        {
            return context.Devices.SingleOrDefaultAsync(d => d.MACAdress == mac);
        }

        public async Task<Device> UpdateDevice(DeviceDTO device, Device existingDevice)
        {
            existingDevice.DistanceA = device.DistanceA;
            existingDevice.DistanceB = device.DistanceB;
            context.Devices.Update(existingDevice);
            await context.SaveChangesAsync();
            return existingDevice;
        }
    }
}
