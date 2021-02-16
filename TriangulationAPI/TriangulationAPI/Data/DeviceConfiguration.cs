using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.Models;

namespace TriangulationAPI.Data
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");

            builder.HasKey(d => d.MACAdress);

            builder.Property(d => d.DistanceA)
                .IsRequired();
            builder.Property(d => d.DistanceB)
                .IsRequired();

            builder.Property(d => d.Latitude)
                .IsRequired();

            builder.Property(d => d.Longitude)
                .IsRequired();
        }
    }
}
