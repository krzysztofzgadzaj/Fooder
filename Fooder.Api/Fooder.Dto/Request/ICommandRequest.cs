using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request
{
    public interface ICommandRequest<out TEntity> where TEntity : BaseEntity
    {
        TEntity CreateEntity();
    }
}
