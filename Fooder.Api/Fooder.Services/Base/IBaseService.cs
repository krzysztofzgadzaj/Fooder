using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.Request;
using Fooder.Dto.ViewModel;
using Fooder.Persistence.Entities;

namespace Fooder.Services.Base
{
    public interface IBaseService<in TEntity, TViewModel> where TEntity : BaseEntity
        where TViewModel : IBuildable<TEntity>, new()
    {
        Task<TViewModel> AddAsync<TCreateRequest>(TCreateRequest createRequest) where TCreateRequest : ICommandRequest<TEntity>;
        Task<TViewModel> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TViewModel>> GetAsync(CancellationToken cancellationToken);
        Task<TViewModel> UpdateAsync<TUpdateRequest>(TUpdateRequest updateRequest, int id)
            where TUpdateRequest : ICommandRequest<TEntity>;
        Task DeleteAsync(int id);
    }
}
