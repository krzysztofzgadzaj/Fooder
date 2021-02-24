using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Fooder.Services.Base;
using Fooder.Repositories.CommentMark;
using Fooder.Dto.Request.Feed.Comment.CommentMark;
using Fooder.Dto.ViewModel;
using System.Threading;

namespace Fooder.Services.Comment
{
    public sealed class CommentService : BaseService<ICommentRepository, CommentEntity, CommentViewModel>,
        ICommentService
    {
        private readonly ICommentMarkRepository _commentMarkRepository;

        public CommentService(ICommentRepository commentRepository, ICommentMarkRepository commentMarkRepository)
            : base(commentRepository)
        {
            _commentMarkRepository = commentMarkRepository;
        }

        public async Task<ICollection<CommentViewModel>> GetCommentsAsync(int feedId, string userName)
        {
            var comments = await Repository.GetFeedCommentsAsync(feedId);

            var viewModels = await FormCommentViewModels(comments, userName);

            return viewModels;
        }

        public async Task<CommentMarkViewModel> AddCommentMarkAsync(CreateCommentMarkRequest request)
        {
            var entity = request.CreateEntity();

            var createdEntity = await _commentMarkRepository.AddAsync(entity);
            var relatedComment = await Repository.GetByIdAsync(request.CommentId);

            relatedComment.CommentMark++;
            Repository.Update(relatedComment);

            await _commentMarkRepository.SaveChangesAsync();
            await Repository.SaveChangesAsync();

            return (CommentMarkViewModel)createdEntity;
        }

        public async Task<CommentMarkViewModel> UpdateCommentMarkAsync(UpdateCommentMarkRequest request, int id)
        {
            var entity = await _commentMarkRepository.GetByIdAsync(id);
            var newEntity = request.CreateEntity();

            entity.Update(newEntity);
            await _commentMarkRepository.SaveChangesAsync();

            return (CommentMarkViewModel)entity;
        }

        public async Task DeleteCommentMarkById(int id)
        {
            await _commentMarkRepository.DeleteByIdAsync(id);
            await _commentMarkRepository.SaveChangesAsync();
        }

        public async Task<ICollection<RankingPositionViewModel>> GetRanking(CancellationToken cancellationToken)
        {
            var commentMarks = await _commentMarkRepository.GetAsync(cancellationToken);
            var ranking = new List<RankingPositionViewModel>();

            var marksByUserName = commentMarks
                    .GroupBy(mark => mark.UserName);

            foreach (var group in marksByUserName)
            {
                var counter = 0;

                foreach (var commentMark in group)
                {
                    if (commentMark.Mark == "like")
                        counter++;
                    else
                        counter--;
                }

                ranking.Add(
                    new RankingPositionViewModel
                    {
                        UserName = group.Key,
                        Points = counter
                    });
            }

            ranking = ranking
                .OrderByDescending(el =>
                    el.Points)
                .ToList();

            return ranking;
        }

        private async Task<ICollection<CommentViewModel>> FormCommentViewModels(ICollection<CommentEntity> comments,
            string userName)
        {
            var commentMarks = await _commentMarkRepository.GetCommentMarks(userName);

            var mainComments = comments
                .Where(comment =>
                    !comment.RelatedCommentId.HasValue)
                .Select(comment =>
                    (CommentViewModel)comment)
                .ToList();

            foreach (var mainComment in mainComments)
            {
                mainComment.RelatedComments = comments
                    .Where(comment =>
                        comment.RelatedCommentId == mainComment.Id)
                            .Select(comment =>
                                (CommentViewModel)comment)
                            .ToList();

                mainComment.UserMark = commentMarks
                        .Where(mark =>
                            mark.CommentId == mainComment.Id)
                        .Select(mark =>
                            mark.Mark)
                        .FirstOrDefault();
            }

            return mainComments;
        }
    }
}

