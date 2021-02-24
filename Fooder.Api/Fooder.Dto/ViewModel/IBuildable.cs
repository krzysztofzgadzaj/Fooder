using Fooder.Persistence.Entities;

namespace Fooder.Dto.ViewModel
{
    public interface IBuildable<in TSource> where TSource : BaseEntity
    {
        void Construct(TSource entity);
    }
}
