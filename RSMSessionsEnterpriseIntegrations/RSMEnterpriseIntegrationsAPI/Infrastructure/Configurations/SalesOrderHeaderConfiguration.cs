namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            builder.ToTable(nameof(SalesOrderHeader), "Sales");

            builder.HasKey(e => e.SalesOrderID);
            builder.Property(e => e.SalesOrderID).HasColumnName("SalesOrderID");
            builder.Property(e => e.RevisionNumber).IsRequired();
            builder.Property(e => e.OrderDate).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.OnlineOrderFlag).IsRequired();
            builder.Property(e => e.CustomerID).IsRequired();
            builder.Property(e => e.BillToAddressID).IsRequired();
            builder.Property(e => e.ShipToAddressID).IsRequired();
            builder.Property(e => e.ShipMethodID).IsRequired();
            builder.Property(e => e.SubTotal).IsRequired();
            builder.Property(e => e.TaxAmt).IsRequired();
            builder.Property(e => e.Freight).IsRequired();
            builder.Property(e => e.ModifiedDate).IsRequired();
            
        }
    }
}
