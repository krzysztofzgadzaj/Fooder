using System;
using Fooder.Constants;
using Fooder.Persistence.Entities;

namespace Fooder.Dto.ViewModel
{
    public sealed class FileViewModel : IBuildable<ImageEntity>
    {
        public string Content { get; set; }
        public string DownloadName { get; set; }
        public string ContentType { get; set; }
        
        public void Construct(ImageEntity entity)
        {
            Content = string.Format(
                FileConstants.FileBase64StringPattern,
                entity.ContentType,
                Convert.ToBase64String(entity.Content));
            DownloadName = entity.Name;
            ContentType = entity.ContentType;
        }
    }
}
