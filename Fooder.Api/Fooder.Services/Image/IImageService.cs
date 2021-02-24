using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Fooder.Services.Image
{
    public interface IImageService : IBaseService<ImageEntity, FileViewModel>
    {
        Task AddImagesAsync(IFormFileCollection images, Guid ownerId);
        Task<ICollection<int>> GetImagesIdsByOwnerGuidAsync(Guid guid, CancellationToken cancellationToken);
    }
}
