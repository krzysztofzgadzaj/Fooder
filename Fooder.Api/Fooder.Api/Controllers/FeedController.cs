using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Api.Utils.AuthorizationPolicies;
using Fooder.Dto.Request.Feed;
using Fooder.Dto.Request.Feed.Comment;
using Fooder.Dto.Request.Feed.Comment.CommentMark;
using Fooder.Dto.Request.Feed.FeedMark;
using Fooder.Dto.Request.Feed.Review;
using Fooder.Dto.ViewModel;
using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Services.Comment;
using Fooder.Services.Feed;
using Fooder.Services.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    [ApiController]
    [Route(RoutePattern)]
    public sealed class FeedController : BaseController
    {
        private readonly IFeedService _feedService;
        private readonly ICommentService _commentService;
        private readonly IReviewService _reviewService;

        public FeedController(IFeedService feedService,
            ICommentService commentService,
            IReviewService reviewService)
        {
            _feedService = feedService;
            _commentService = commentService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FeedViewModel>>> GetFeedCollectionAsync(CancellationToken cancellationToken,
            string userName = null)
        {
            var result = await _feedService.GetAsync(cancellationToken, userName);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FeedAdditionPermittedPolicy))]
        public async Task<ActionResult<FeedViewModel>> CreateFeedAsync([FromQuery] CreateFeedRequest request)
        {
            request.Photos = Request.Form.Files;
            var createdEntity = await _feedService.AddAsync(request);
            return Created(string.Format(CreateEntityPattern, RoutePattern, createdEntity.Id), createdEntity);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFeedAsync([FromRoute] int id)
        {
            await _feedService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FeedViewModel>> UpdateFeedAsync(
            [FromBody] UpdateFeedRequest request,
            [FromRoute] int id)
        {
            var result = await _feedService.UpdateAsync(request, id);
            return Ok(result);
        }

        [HttpPost("{feedId:int}/feedMarks")]
        public async Task<ActionResult<FeedMarkViewModel>> CreateFeedMarkAsync([FromBody] CreateFeedMarkRequest request)
        {
            var createdEntity = await _feedService.AddFeedMarkAsync(request);
            return Created(string.Format(CreateEntityPattern, RoutePattern, createdEntity.Id), createdEntity);
        }

        [HttpPut("{feedId:int}/feedMarks")]
        public async Task<ActionResult<FeedMarkViewModel>> UpdateFeedMarkAsync(
            [FromBody] UpdateFeedMarkRequest request,
            int id)
        {
            var result = await _feedService.UpdateFeedMarkAsync(request, id);
            return Ok(result);
        }

        [HttpDelete("{feedId:int}/feedMarks")]
        public async Task<IActionResult> DeleteFeedMarkById(int id)
        {
            await _feedService.DeleteFeedMarkById(id);
            return Ok();
        }

        [HttpPost("{feedId:int}/comments")]
        public async Task<ActionResult<CommentViewModel>> CreateCommentAsync([FromBody] CreateCommentRequest request)
        {
            var createdEntity = await _commentService.AddAsync(request);
            return Created(string.Format(CreateEntityPattern, RoutePattern, createdEntity.Id), createdEntity);
        }

        [HttpGet("{feedId:int}/comments")]
        public async Task<ActionResult<ICollection<CommentViewModel>>> GetCommentsAsync([FromRoute] int feedId,
            [FromQuery] string userName = null)
        {
            var result = await _commentService.GetCommentsAsync(feedId, userName);
            return Ok(result);
        }

        [HttpDelete("{feedId:int}/comments/{id:int}")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            await _commentService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{feedId:int}/comments/{id:int}")]
        public async Task<ActionResult<FeedViewModel>> UpdateCommentAsync(
            [FromBody] UpdateCommentRequest request,
            [FromRoute] int id)
        {
            var result = await _commentService.UpdateAsync(request, id);
            return Ok(result);
        }

        [HttpPost("{feedId:int}/commentMarks")]
        public async Task<ActionResult<CommentMarkViewModel>> CreateCommentMarkAsync([FromBody] CreateCommentMarkRequest request)
        {
            var createdEntity = await _commentService.AddCommentMarkAsync(request);
            return Created(string.Format(CreateEntityPattern, RoutePattern, createdEntity.Id), createdEntity);
        }

        [HttpPut("{feedId:int}/commentMarks")]
        public async Task<ActionResult<CommentMarkViewModel>> UpdateCommentMarkAsync(
            [FromBody] UpdateCommentMarkRequest request,
            int id)
        {
            var result = await _commentService.UpdateCommentMarkAsync(request, id);
            return Ok(result);
        }

        [HttpDelete("{feedId:int}/commentMarks")]
        public async Task<IActionResult> DeleteCommentMarkById(int id)
        {
            await _commentService.DeleteCommentMarkById(id);
            return Ok();
        }

        [HttpPost("reviews")]
        [Authorize(Policy = nameof(ReviewAdditionPermittedPolicy))]
        public async Task<ActionResult<ReviewViewModel>> CreateReviewAsync([FromQuery] CreateReviewRequest request)
        {
            var createdEntity = await _reviewService.AddAsync(request);
            return Created(string.Format(CreateEntityPattern, RoutePattern, createdEntity.Id), createdEntity);
        }

        [HttpGet("{feedId:int}/reviews")]
        public async Task<ActionResult<ICollection<ReviewViewModel>>> GetReviewsAsync([FromRoute] int feedId)
        {
            var result = await _reviewService.GetRangeAsync(feedId);
            return Ok(result);
        }

        [HttpDelete("{feedId:int}/reviews/{id:int}")]
        public async Task<IActionResult> DeleteReviewById(int id)
        {
            await _reviewService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{feedId:int}/reviews/{id:int}")]
        public async Task<ActionResult<FeedViewModel>> UpdateReviewAsync(
            [FromBody] UpdateReviewRequest request,
            [FromRoute] int id)
        {
            var result = await _reviewService.UpdateAsync(request, id);
            return Ok(result);
        }

        [HttpGet("ranking")]
        public async Task<ActionResult<ICollection<RankingPositionViewModel>>> GetRankingAsync(CancellationToken cancellationToken)
        {
            var result = await _commentService.GetRanking(cancellationToken);
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetFeedTypesAsync(CancellationToken cancellationToken) =>
            Ok(await Task.Run(
                () => Enum.GetValues(typeof(FeedType))
                    .Cast<FeedType>()
                    .Select(
                        type => new
                        {
                            Name = type.ToString(),
                            Key = type,
                        })
                    .ToList(),
                cancellationToken));
    }
}
