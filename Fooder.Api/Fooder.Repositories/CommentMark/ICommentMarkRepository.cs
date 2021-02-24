using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fooder.Repositories.CommentMark
{
    public interface  ICommentMarkRepository : IBaseRepository<CommentMarkEntity>
    {
        Task<ICollection<CommentMarkEntity>> GetCommentMarks(string userName);
    }
}
