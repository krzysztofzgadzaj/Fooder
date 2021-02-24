using Fooder.Persistence.Entities;

namespace Fooder.Dto.ViewModel.Feed
{
    public sealed class ReviewViewModel : IBuildable<ReviewEntity>
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public void Construct(ReviewEntity entity)
        {
            Id = entity.Id;
            Content = entity.Content;
        }
    }
}
