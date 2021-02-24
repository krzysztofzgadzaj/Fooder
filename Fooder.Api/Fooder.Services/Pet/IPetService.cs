using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;

namespace Fooder.Services.Pet
{
    public interface IPetService : IBaseService<PetEntity, PetViewModel>
    {
        Task<ICollection<PetViewModel>> GetOwnerPetsAsync(int ownerId, CancellationToken cancellationToken);
    }
}
