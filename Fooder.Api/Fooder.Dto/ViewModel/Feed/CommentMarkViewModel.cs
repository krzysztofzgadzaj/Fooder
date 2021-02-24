using Fooder.Persistence.Entities;
using System;

namespace Fooder.Dto.ViewModel.Feed
{
    public sealed class CommentMarkViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CommentId { get; set; }
        public string Mark { get; set; }

        public static explicit operator CommentMarkViewModel(CommentMarkEntity v) =>
            new CommentMarkViewModel
            {
                UserName = v.UserName,
                CommentId = v.CommentId,
                Mark = v.Mark,
                Id = v.Id
            };
    }
}
