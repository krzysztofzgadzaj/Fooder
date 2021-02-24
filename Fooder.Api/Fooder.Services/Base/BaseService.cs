using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Constants;
using Fooder.Dto.Request;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;
using Fooder.Persistence.Entities.Interface;
using Fooder.Repositories.Base;

namespace Fooder.Services.Base
{
    public abstract class BaseService<TRepository, TEntity, TViewModel> : IBaseService<TEntity, TViewModel>
        where TRepository : IBaseRepository<TEntity>
        where TEntity : BaseEntity, IUpdatable<TEntity>
        where TViewModel : IBuildable<TEntity>, new()
    {
        protected BaseService(TRepository repository)
        {
            Repository = repository;
        }

        protected TRepository Repository { get; }

        public virtual async Task<TViewModel> AddAsync<TCreateRequest>(TCreateRequest createRequest)
            where TCreateRequest : ICommandRequest<TEntity>
        {
            var entity = createRequest.CreateEntity();
            var createdEntity = await Repository.AddAsync(entity);

            await Repository.SaveChangesAsync();

            var viewModel = new TViewModel();
            viewModel.Construct(createdEntity);

            return viewModel;
        }

        public virtual async Task<TViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            var doesExist = !(entity is null);

            if (!doesExist)
            {
                var exceptionMessage = string.Format(
                    Messages.EntityNotFoundExceptionMessagePattern,
                    id,
                    typeof(TViewModel).Name);

                throw new KeyNotFoundException(exceptionMessage);
            }

            var viewModel = new TViewModel();
            viewModel.Construct(entity);

            return viewModel;
        }

        public virtual async Task<IEnumerable<TViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            var entities = await Repository.GetAsync(cancellationToken);
            var viewModels = new List<TViewModel>();

            foreach (var entity in entities)
            {
                var viewModel = new TViewModel();
                viewModel.Construct(entity);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public virtual async Task<TViewModel> UpdateAsync<TUpdateRequest>(TUpdateRequest updateRequest, int id)
            where TUpdateRequest : ICommandRequest<TEntity>
        {
            var entity = await Repository.GetByIdAsync(id);
            var doesExist = !(entity is null);

            if (!doesExist)
            {
                var exceptionMessage = string.Format(
                    Messages.EntityNotFoundExceptionMessagePattern,
                    id,
                    typeof(TViewModel).Name);

                throw new KeyNotFoundException(exceptionMessage);
            }

            var newEntity = updateRequest.CreateEntity();
            entity.Update(newEntity);

            await Repository.SaveChangesAsync();

            var viewModel = new TViewModel();
            viewModel.Construct(entity);

            return viewModel;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            var doesExist = !(entity is null);

            if (!doesExist)
            {
                var exceptionMessage = string.Format(
                    Messages.EntityNotFoundExceptionMessagePattern,
                    id,
                    typeof(TViewModel).Name);

                throw new KeyNotFoundException(exceptionMessage);
            }

            await Repository.DeleteByIdAsync(id);
            await Repository.SaveChangesAsync();
        }

        protected static string GetImproperRequestErrorMessage(MemberInfo requestType, string serviceName) =>
            string.Format(Messages.ImproperRequestExceptionMessagePattern, requestType.Name, serviceName);
    }
}
