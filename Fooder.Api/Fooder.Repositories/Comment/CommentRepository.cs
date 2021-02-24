using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fooder.Repositories.Comment
{
    public sealed class CommentRepository : BaseRepository<CommentEntity, FooderContext>, 
        ICommentRepository
    {
        public CommentRepository(FooderContext context)
            : base(context)
        {
        }

        public async Task<ICollection<CommentEntity>> GetFeedCommentsAsync(int feedId) =>
            await DbContext.Comments
                .Where(comment => 
                    comment.FeedEntityId == feedId)
                .ToListAsync();
    }
}
