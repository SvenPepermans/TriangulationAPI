using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.DTOs;
using TriangulationAPI.Models;
using TriangulationAPI.Services;

namespace TriangulationAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceServices deviceServices;

        public DeviceController(IDeviceServices deviceServices)
        {
            this.deviceServices = deviceServices;
        }

        /// <summary>
        /// Gets the device with given mac address
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The device with given id or NotFound if no device with given mac address</returns>
        [HttpGet("Devices/{mac}")]
        public async Task<ActionResult<Device>> GetDeviceByMAC(string mac)
        {
            var device = await deviceServices.GetDeviceByMAC(mac);

            if (device == null)
            {
                return NotFound();
            }
            
            return device;
        }

        /// <summary>
        /// Gets all the devices from the database
        /// </summary>
        /// <returns>List of devices</returns>
        [HttpGet]
        public Task<IEnumerable<Device>> GetDevices()
        {           
            return deviceServices.GetAllDevices();
        }

        /// <summary>
        /// Add a new device
        /// </summary>
        /// <param name="device"></param>
        /// <returns>status and newly created device</returns>
        [HttpPost("Add/{Device}")]
        public async Task<ActionResult<Device>> AddDevice(DeviceDTO device)
        {
            var existingDevice = await deviceServices.GetDeviceByMAC(device.MACAdress);
            if (existingDevice == null)
            {
                var deviceToAdd = await deviceServices.AddDevice(device);
                if (deviceToAdd != null)
                {
                    return deviceToAdd;
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                await deviceServices.UpdateDevice(device, existingDevice);
                return existingDevice;
            }
           
        }
    }
}
