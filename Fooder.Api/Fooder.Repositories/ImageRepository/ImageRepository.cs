using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fooder.Repositories.ImageRepository
{
    public sealed class ImageRepository : BaseRepository<ImageEntity, FooderContext>,
        IImageRepository
    {
        public ImageRepository(FooderContext context)
            : base(context)
        {
        }
        
        public async Task<ICollection<int>> GetImagesIdsByOwnerGuidAsync(Guid guid, CancellationToken cancellationToken) =>
            await DbContext
                .Images
                .Where(image =>
                    image.OwnerId == guid)
                .Select(image => image.Id)
                .ToListAsync(cancellationToken);
    }
}
