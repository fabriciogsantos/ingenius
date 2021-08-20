using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;


namespace Ingenius.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);

            builder.ToTable("Productos");
        }
    }
}
