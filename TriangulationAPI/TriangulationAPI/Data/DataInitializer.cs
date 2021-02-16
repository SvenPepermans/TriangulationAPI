using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            }
        }
    }
}
