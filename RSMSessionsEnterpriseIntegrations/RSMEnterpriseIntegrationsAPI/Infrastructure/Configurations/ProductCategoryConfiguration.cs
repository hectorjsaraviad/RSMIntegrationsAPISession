﻿namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ProductCategory), "Production");

            builder.HasKey(e => e.ProductCategoryID);
            builder.Property(e => e.ProductCategoryID).HasColumnName("ProductCategoryID");
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.ModifiedDate);
        }
    }
}
