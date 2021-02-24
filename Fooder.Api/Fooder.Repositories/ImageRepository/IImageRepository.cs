using Fooder.Repositories.Base;
using Fooder.Persistence.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Fooder.Repositories.ImageRepository
{
    public interface IImageRepository : IBaseRepository<ImageEntity>
    {
        Task<ICollection<int>> GetImagesIdsByOwnerGuidAsync(Guid guid, CancellationToken cancellationToken);
    }
}
