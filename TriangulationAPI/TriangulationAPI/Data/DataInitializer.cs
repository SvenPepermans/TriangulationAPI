using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.Models;

namespace TriangulationAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public DataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task InitializeDataAsync()
        {
            Console.WriteLine("Deleting db...");
            await context.Database.EnsureDeletedAsync();
            Console.WriteLine("Creating db...");
            if(await context.Database.EnsureCreatedAsync())
            {
                var molenstraat = new AccessPoint()
                {
                    Latitude = 50.938508,                                      
                    Longitude = 4.039638
                };

                var dirkMartens = new AccessPoint()
                {
                    Latitude = 50.938404,                                                                 
                    Longitude = 4.038678                   
                };
                var kerkstraat = new AccessPoint()
                {
                    Latitude = 50.938167,
                    Longitude = 4.039691
                };
                await context.AccessPoints.AddAsync(molenstraat);
                await context.AccessPoints.AddAsync(dirkMartens);
                await context.AccessPoints.AddAsync(kerkstraat);

                await context.SaveChangesAsync();

                var device1 = new Device()
                {
                    MACAdress = "AAA-111",
                    DistanceA = 55,
                    DistanceB = 55
                };

                var device2 = new Device()
                {
                    MACAdress = "DEV-666",
                    DistanceA = 50,
                    DistanceB = 60
                };

                await context.Devices.AddAsync(device2);
                await context.Devices.AddAsync(device1);
                await context.SaveChangesAsync();

            }
        }
    }
}
