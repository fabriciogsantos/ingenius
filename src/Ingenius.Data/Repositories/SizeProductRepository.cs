using Ingenius.Data.Context;
using Ingenius.Domain;

namespace Ingenius.Data.Repositories
{
    public class SizeProductRepository : RepositoryBase<SizeProduct>
    {
        public SizeProductRepository(IngeniusContext context) : base(context)
        {
        }
    }
}
