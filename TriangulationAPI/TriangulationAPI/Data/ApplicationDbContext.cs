using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.Models;

namespace TriangulationAPI.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<AccessPoint> AccessPoints { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DeviceConfiguration());
            builder.ApplyConfiguration(new AccessPointConfiguration());
        }
    }
}
