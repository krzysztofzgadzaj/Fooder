using Fooder.Persistence.Entities;
using System;

namespace Fooder.Dto.ViewModel.Feed
{
    public class FeedMarkViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int FeedId { get; set; }
        public int Mark { get; set; }

        public static explicit operator FeedMarkViewModel(FeedMarkEntity v) =>
            new FeedMarkViewModel
            {
                Id = v.Id,
                UserName = v.UserName,
                FeedId = v.FeedId,
                Mark = v.Mark,
            };
    }
}

