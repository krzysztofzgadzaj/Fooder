using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;

namespace Fooder.Repositories.Pet
{
    public interface IPetRepository : IBaseRepository<PetEntity>
    {
        Task<ICollection<PetEntity>> GetOwnerPetsAsync(int ownerId, CancellationToken cancellationToken);
        Task<PetEntity> GetPetWithAfflictionsByIdAsync(int id);
    }
}
