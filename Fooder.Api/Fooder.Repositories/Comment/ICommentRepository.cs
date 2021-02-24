using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fooder.Repositories.Comment
{
    public interface ICommentRepository : IBaseRepository<CommentEntity>
    {
        Task<ICollection<CommentEntity>> GetFeedCommentsAsync(int feedId);        
    }
}

