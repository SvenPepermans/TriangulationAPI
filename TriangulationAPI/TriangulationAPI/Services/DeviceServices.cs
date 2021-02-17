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
            if (device.DistanceA != 0)
            {
                existingDevice.DistanceA = device.DistanceA;            
            }
            if (device.DistanceB != 0)
            {
                existingDevice.DistanceB = device.DistanceB;
            }
            context.Devices.Update(existingDevice);
            await context.SaveChangesAsync();
            return existingDevice;
        }

        public async Task CalculatePosition(Device device)
        {
            var AP1 = await context.AccessPoints.OrderBy(d => d.Id).FirstOrDefaultAsync();
            var AP2 = await context.AccessPoints.OrderBy(d => d.Id).LastOrDefaultAsync();

            var cx0 = AP1.Latitude;
            var cy0 = AP1.Longitude;
            var radius0 = device.DistanceA * 0.0001;

            var cx1 = AP2.Latitude;
            var cy1 = AP2.Longitude;
            var radius1 = device.DistanceB * 0.0001;

            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Math.Sqrt(dx * dx + dy * dy);

            if (dist > radius0 + radius1)
            { }
            else if (dist < Math.Abs(radius0 - radius1))
            { }
            else if ((dist == 0) && (radius0 == radius1))
            { }
            else
            {


                // Find a and h.
                var a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                var h = Math.Sqrt((radius0 * radius0) - (a * a));

                // Find P2.
                var cx2 = cx0 + a * (cx1 - cx0) / dist;
                var cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                var xIntersection1 = (cx2 + h * (cy1 - cy0) / dist);
                var yIntersction1 = (cy2 - h * (cx1 - cx0) / dist);

                var xIntersection2 = (cx2 - h * (cy1 - cy0) / dist);
                var yIntersection2 = (cy2 + h * (cx1 - cx0) / dist);

                if (dist == radius0 + radius1)
                {
                    device.Latitude = xIntersection1;
                    device.Longitude = yIntersction1;
                }
                else
                {
                    var avgx = (xIntersection1 + xIntersection2) / 2;
                    var avgy = (yIntersction1 + yIntersection2) / 2;

                    device.Latitude = avgx;
                    device.Longitude = avgy;

                }
                context.Devices.Update(device);
                await context.SaveChangesAsync();
            }
        }

        // Find the points where the two circles intersect.
       
    }
}
