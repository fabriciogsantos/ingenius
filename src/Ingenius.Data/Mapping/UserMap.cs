using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;


namespace Ingenius.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);
            builder.Ignore(a => a.CheckPassword);

            builder.ToTable("Usuarios");
        }
    }
}
