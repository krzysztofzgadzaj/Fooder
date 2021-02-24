using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.Request.Feed;
using Fooder.Dto.Request.Feed.FeedMark;
using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Feed;
using Fooder.Repositories.FeedMark;
using Fooder.Services.Base;
using Fooder.Services.Image;

namespace Fooder.Services.Feed
{
    public sealed class FeedService : BaseService<IFeedRepository, FeedEntity, FeedViewModel>,
        IFeedService
    {
        private readonly IImageService _imageService;
        private IFeedMarkRepository _feedMarkRepository;

        public FeedService(IFeedRepository feedRepository,
            IImageService imageService,
            IFeedMarkRepository feedMarkRepository)
                : base(feedRepository)
        {
            _imageService = imageService;
            _feedMarkRepository = feedMarkRepository;
        }

        public override async Task<FeedViewModel> AddAsync<TCreateRequest>(TCreateRequest createRequest)
        {
            if (!(createRequest is CreateFeedRequest request))
            {
                var message = GetImproperRequestErrorMessage(typeof(TCreateRequest), nameof(IFeedService));
                throw new ArgumentException(message);
            }

            var entity = createRequest.CreateEntity();
            var createdEntity = await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            var tempEntity = await Repository.GetFeedWithAfflictionsById(createdEntity.Id);

            createdEntity.DogAfflictions = tempEntity.DogAfflictions;
            var viewModel = new FeedViewModel();
            viewModel.Construct(createdEntity);

            await _imageService.AddImagesAsync(request.Photos, viewModel.UniqueId);

            viewModel.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(
                viewModel.UniqueId,
                CancellationToken.None);

            return viewModel;
        }

        public async Task<IEnumerable<FeedViewModel>> GetAsync(CancellationToken cancellationToken,
            string userName)
        {
            var feedCollection = (await base.GetAsync(cancellationToken))
                .ToList();

            var feedMarks = await _feedMarkRepository.GetAsync(cancellationToken);

            foreach (var feed in feedCollection)
            {
                feed.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(
                    feed.UniqueId,
                    CancellationToken.None);

                feed.UserMark = feedMarks
                    .Where(mark =>
                        mark.UserName == userName)
                    .Where(mark =>
                        mark.FeedId == feed.Id)
                    .Select(mark =>
                        mark.Mark)
                    .FirstOrDefault();

                feed.RatingCount = feedMarks
                    .Count(mark => mark.FeedId == feed.Id);

                feed.AverageRating = feed.RatingCount > 0
                    ? (float)feedMarks
                        .Where(mark => mark.FeedId == feed.Id)
                        .Average(mark => mark.Mark)
                    : 0;
            }

            return feedCollection;
        }

        public override async Task<FeedViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var feed = await base.GetByIdAsync(id, cancellationToken);
            feed.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(feed.UniqueId, CancellationToken.None);

            return feed;
        }

        public override async Task DeleteAsync(int id)
        {
            var feed = await GetByIdAsync(id, CancellationToken.None);

            foreach (var photoId in feed.PhotosIds)
            {
                await _imageService.DeleteAsync(photoId);
            }

            await base.DeleteAsync(id);
        }

        public async Task<FeedMarkViewModel> AddFeedMarkAsync(CreateFeedMarkRequest request)
        {
            var entity = request.CreateEntity();

            var createdEntity = await _feedMarkRepository.AddAsync(entity);
            await _feedMarkRepository.SaveChangesAsync();

            return (FeedMarkViewModel)createdEntity;
        }

        public async Task<FeedMarkViewModel> UpdateFeedMarkAsync(UpdateFeedMarkRequest request, int id)
        {
            var entity = await _feedMarkRepository.GetByIdAsync(id);
            var newEntity = request.CreateEntity();

            entity.Update(newEntity);
            await _feedMarkRepository.SaveChangesAsync();

            return (FeedMarkViewModel)entity;
        }

        public async Task DeleteFeedMarkById(int id)
        {
            await _feedMarkRepository.DeleteByIdAsync(id);
            await _feedMarkRepository.SaveChangesAsync();
        }
    }
}
