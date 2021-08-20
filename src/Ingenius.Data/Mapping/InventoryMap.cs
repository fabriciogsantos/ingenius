using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;


namespace Ingenius.Data.Mapping
{
    public class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Inventory> builder)
        {
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);

            builder.ToTable("Stock");
        }
    }
}
