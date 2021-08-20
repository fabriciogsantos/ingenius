using Ingenius.Data.Context;
using Ingenius.Domain;

namespace Ingenius.Data.Repositories
{
    public class InventoryRepository : RepositoryBase<Inventory>
    {
        public InventoryRepository(IngeniusContext context) : base(context)
        {
        }
    }
}
