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
                var AP1 = new AccessPoint()
                {
                    Latitude = 50.815782,                   
                    Longitude = 3.920818
                };

                var AP2 = new AccessPoint()
                {
                    Latitude = 50.815602,                   
                    Longitude = 3.921102
                };
                await context.AccessPoints.AddAsync(AP1);
                await context.AccessPoints.AddAsync(AP2);
                await context.SaveChangesAsync();

                var device1 = new Device()
                {
                    MACAdress = "AAA-111",
                    DistanceA = 30,
                    DistanceB = 5
                };

                await context.Devices.AddAsync(device1);
                await context.SaveChangesAsync();

            }
        }
    }
}
