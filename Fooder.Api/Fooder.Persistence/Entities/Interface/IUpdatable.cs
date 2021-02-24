namespace Fooder.Persistence.Entities.Interface
{
    public interface IUpdatable<in TEntity> where TEntity : BaseEntity
    {
        void Update(TEntity entity);
    }
}
