using Fooder.Persistence.Entities;

namespace Fooder.Dto.ViewModel
{
    public sealed class BrandViewModel : IBuildable<BrandEntity>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public void Construct(BrandEntity brandEntity)
        {
            Id = brandEntity.Id;
            Name = brandEntity.Name;
        }
    }
}
