using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fooder.Repositories.Pet
{
    public sealed class PetRepository : BaseRepository<PetEntity, FooderContext>,
        IPetRepository
    {
        public PetRepository(FooderContext context)
            : base(context)
        {
        }

        public async Task<ICollection<PetEntity>> GetOwnerPetsAsync(int ownerId, CancellationToken cancellationToken) =>
            await DbContext
                .Pets
                .Include(x => x.PetAfflictions)
                    .ThenInclude(x => x.AfflictionEntity)
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync(cancellationToken);

        public override async Task<ICollection<PetEntity>> GetAsync(CancellationToken cancellationToken) =>
         await DbContext.Pets
                .Include(pet => pet.PetAfflictions)
                    .ThenInclude(petAffliction => petAffliction.AfflictionEntity)
                .ToListAsync(cancellationToken);

        public async Task<PetEntity> GetPetWithAfflictionsByIdAsync(int id) =>
            await DbContext.Pets
                .Include(pet => pet.PetAfflictions)
                    .ThenInclude(petAffliction => petAffliction.AfflictionEntity)
                .FirstOrDefaultAsync(pet => pet.Id == id);
    }
}
