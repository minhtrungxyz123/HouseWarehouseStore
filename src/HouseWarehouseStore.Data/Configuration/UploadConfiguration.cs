using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseWarehouseStore.Data.Configuration
{
    public class UploadConfiguration : IEntityTypeConfiguration<Upload>
    {
        public void Configure(EntityTypeBuilder<Upload> entity)
        {
            entity.ToTable("Uploads");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);
        }
    }
}