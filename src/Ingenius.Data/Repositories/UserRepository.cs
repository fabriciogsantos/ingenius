using Ingenius.Data.Context;
using Ingenius.Domain;

namespace Ingenius.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(IngeniusContext context) : base(context)
        {
        }
    }
}
