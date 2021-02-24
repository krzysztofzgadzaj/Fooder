using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Helpers;
using Fooder.Persistence.Entities;
using Fooder.Repositories.ImageRepository;
using Fooder.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Fooder.Services.Image
{
    public sealed class ImageService : BaseService<IImageRepository, ImageEntity, FileViewModel>,
        IImageService
    {
        public ImageService(IImageRepository imageRepository)
            : base(imageRepository)
        {
        }

        public async Task AddImagesAsync(IFormFileCollection images, Guid ownerId)
        {
            var imageEntities = await Task
                .WhenAll(images
                    .Select(async image => new ImageEntity
                        {
                            Name = image.FileName,
                            Content = await FileManager.SerializeFormFileAsync(image),
                            ContentType = image.ContentType,
                            OwnerId = ownerId,
                        }));
            
            await Repository.AddRangeAsync(imageEntities);
            await Repository.SaveChangesAsync();
        }

        public async Task<ICollection<int>> GetImagesIdsByOwnerGuidAsync(Guid guid, CancellationToken cancellationToken) =>
            await Repository.GetImagesIdsByOwnerGuidAsync(guid, cancellationToken);
    }
}
