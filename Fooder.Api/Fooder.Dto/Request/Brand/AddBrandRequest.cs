using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Brand
{
    public sealed class AddBrandRequest : ICommandRequest<BrandEntity>
    {
        public string Name { get; set; }

        public BrandEntity CreateEntity() =>
            new BrandEntity
            {
                Name = Name,
            };

    }
}
