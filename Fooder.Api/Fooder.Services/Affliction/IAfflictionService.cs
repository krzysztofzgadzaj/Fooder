using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Dto.ViewModel;

namespace Fooder.Services.Affliction
{
    public interface IAfflictionService
    {
        Task<IEnumerable<AfflictionViewModel>> GetRangeAsync(CancellationToken cancellationToken);
    }
}
