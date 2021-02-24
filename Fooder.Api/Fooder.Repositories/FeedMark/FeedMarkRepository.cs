using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fooder.Repositories.FeedMark
{
    public class FeedMarkRepository : BaseRepository<FeedMarkEntity, FooderContext>,
        IFeedMarkRepository
    {
        public FeedMarkRepository(FooderContext context)
            : base(context)
        {
        }

        public async Task<ICollection<FeedMarkEntity>> GetFeedMarksByUserNameAsync(string userName) =>
            await DbContext.FeedMarks
                .Where(mark => 
                    mark.UserName == userName)
                .ToListAsync();
    }
}
