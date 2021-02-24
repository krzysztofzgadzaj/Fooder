using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;

namespace Fooder.Services.Brand
{
    public interface IBrandService : IBaseService<BrandEntity, BrandViewModel>
    {
    }
}
