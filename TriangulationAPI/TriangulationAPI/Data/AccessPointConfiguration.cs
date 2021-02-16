using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriangulationAPI.Models;

namespace TriangulationAPI.Data
{
    public class AccessPointConfiguration : IEntityTypeConfiguration<AccessPoint>
    {
        public void Configure(EntityTypeBuilder<AccessPoint> builder)
        {
            builder.ToTable("AccessPoints");
            builder.HasKey(ap => ap.Id);

            builder.Property(d => d.Latitude)
                .IsRequired();

            builder.Property(d => d.Longitude)
                .IsRequired();
        }
    }
}
