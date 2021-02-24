using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;
using Fooder.Repositories.Afflictions;

namespace Fooder.Services.Affliction
{
    public sealed class AfflictionService : IAfflictionService
    {
        private readonly IAfflictionRepository _afflictionRepository;

        public AfflictionService(IAfflictionRepository afflictionRepository)
        {
            _afflictionRepository = afflictionRepository;
        }

        public async Task<IEnumerable<AfflictionViewModel>> GetRangeAsync(CancellationToken cancellationToken)
        {
            var result = await _afflictionRepository.GetRangeAsync(cancellationToken);
            return result.Select(x => new AfflictionViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });
        }
    }
}
