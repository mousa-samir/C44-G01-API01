using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {

        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(X => X.Name)
                    .HasMaxLength(100);
        }
    }
}
