using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.Request.Pet;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Pet;
using Fooder.Services.Base;
using Fooder.Services.Image;

namespace Fooder.Services.Pet
{
    public sealed class PetService : BaseService<IPetRepository, PetEntity, PetViewModel>,
        IPetService
    {
        private readonly IImageService _imageService;

        public PetService(IPetRepository petRepository,
            IImageService imageService)
            : base(petRepository)
        {
            _imageService = imageService;
        }

        public async Task<ICollection<PetViewModel>> GetOwnerPetsAsync(int ownerId, CancellationToken cancellationToken)
        {
            var entities = await Repository.GetOwnerPetsAsync(ownerId, cancellationToken);
            var results = new List<PetViewModel>();

            foreach (var entity in entities)
            {
                var viewModel = new PetViewModel();
                viewModel.Construct(entity);
                viewModel.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(
                    viewModel.UniqueId,
                    CancellationToken.None);

                results.Add(viewModel);
            }

            return results;
        }

        public override async Task<PetViewModel> AddAsync<TCreateRequest>(TCreateRequest createRequest)
        {
            if (!(createRequest is AddPetRequest request))
            {
                var message = GetImproperRequestErrorMessage(typeof(TCreateRequest), nameof(IPetService));
                throw new ArgumentException(message);
            }

            var entity = createRequest.CreateEntity();
            var createdEntity = await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            var tempEntity = await Repository.GetPetWithAfflictionsByIdAsync(createdEntity.Id);

            createdEntity.PetAfflictions = tempEntity.PetAfflictions;
            var viewModel = new PetViewModel();
            viewModel.Construct(createdEntity);

            await _imageService.AddImagesAsync(request.Photos, viewModel.UniqueId);

            viewModel.PhotosIds =
                await _imageService.GetImagesIdsByOwnerGuidAsync(viewModel.UniqueId, CancellationToken.None);

            return viewModel;
        }

        public override async Task<IEnumerable<PetViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            var pets = (await base.GetAsync(cancellationToken))
                .ToList();

            foreach (var pet in pets)
            {
                pet.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(
                    pet.UniqueId,
                    CancellationToken.None);
            }

            return pets;
        }

        public override async Task<PetViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var pet = await base.GetByIdAsync(id, cancellationToken);
            pet.PhotosIds = await _imageService.GetImagesIdsByOwnerGuidAsync(pet.UniqueId, CancellationToken.None);

            return pet;
        }

        public override async Task DeleteAsync(int id)
        {
            var pet = await GetByIdAsync(id, CancellationToken.None);

            foreach (var photoId in pet.PhotosIds)
            {
                await _imageService.DeleteAsync(photoId);
            }

            await base.DeleteAsync(id);
        }
    }
}
