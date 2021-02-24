using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fooder.Repositories.CommentMark
{
    public sealed class CommentMarkRepository : BaseRepository<CommentMarkEntity, FooderContext>,
        ICommentMarkRepository
    {
        public CommentMarkRepository(FooderContext context)
            : base(context)
        {
        }

        public async Task<ICollection<CommentMarkEntity>> GetCommentMarks(string userName) =>
            await DbContext.CommentMarks
            .Where(mark => 
                mark.UserName == userName)
            .ToListAsync();
    }
}
