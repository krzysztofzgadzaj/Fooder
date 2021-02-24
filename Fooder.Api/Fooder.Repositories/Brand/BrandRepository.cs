using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;

namespace Fooder.Repositories.Brand
{
    public sealed class BrandRepository : BaseRepository<BrandEntity, FooderContext>,
        IBrandRepository
    {
        public BrandRepository(FooderContext context)
            : base(context)
        {
        }
    }
}
