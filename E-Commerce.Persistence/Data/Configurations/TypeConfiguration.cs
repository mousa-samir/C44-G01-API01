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
    public class TypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(X => X.Name)
                    .HasMaxLength(100);
        }
    }
}
