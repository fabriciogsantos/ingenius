using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ingenius.Data.Mapping
{

    public class SizeProductMap : IEntityTypeConfiguration<SizeProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SizeProduct> builder)
        {
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);

            builder.ToTable("SizeProductos");
        }
    }
}
