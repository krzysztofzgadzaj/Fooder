using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Brand;
using Fooder.Services.Base;

namespace Fooder.Services.Brand
{
    public sealed class BrandService : BaseService<IBrandRepository, BrandEntity, BrandViewModel>,
        IBrandService
    {
        public BrandService(IBrandRepository repository)
            : base(repository)
        {
        }
    }
}
