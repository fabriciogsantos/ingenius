using Ingenius.Data.Context;
using Ingenius.Domain;

namespace Ingenius.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IngeniusContext context) : base(context)
        {
        }
    }
}
