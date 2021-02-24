using Fooder.Dto.Request.Feed.Comment.CommentMark;
using Fooder.Dto.ViewModel;
using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fooder.Services.Comment
{
    public interface ICommentService : IBaseService<CommentEntity, CommentViewModel>
    {
        Task<ICollection<CommentViewModel>> GetCommentsAsync(int feedId, string userName);
        Task<CommentMarkViewModel> AddCommentMarkAsync(CreateCommentMarkRequest request);
        Task<CommentMarkViewModel> UpdateCommentMarkAsync(UpdateCommentMarkRequest request, int id);
        Task DeleteCommentMarkById(int id);
        Task<ICollection<RankingPositionViewModel>> GetRanking(CancellationToken cancellationToken);
    }
}

