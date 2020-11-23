using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using Website.Data.Contexts;
using Website.Data.Entities;

namespace Website.Services.Data_Loaders
{
    /// <summary>
    /// Data loader for posts, using their ID.
    /// </summary>
    /// <seealso cref="HotChocolate.DataLoader.BatchDataLoader{int, Website.Data.Entities.Post}" />
    public sealed class PostByIdDataLoader : BatchDataLoader<int, Post>
    {
        /// <summary>
        /// Gets the context factory.
        /// </summary>
        /// <value>
        /// The context factory.
        /// </value>
        private IDbContextFactory<ApiContext> ContextFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostByIdDataLoader"/> class.
        /// </summary>
        /// <param name="batchScheduler">The batch scheduler.</param>
        /// <param name="dbContextFactory">The database context factory.</param>
        public PostByIdDataLoader(IBatchScheduler batchScheduler,
                                  IDbContextFactory<ApiContext> dbContextFactory)
            : base(batchScheduler)
        {
            ContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Loads the batch asynchronous.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Dictionary of posts.</returns>
        protected async override Task<IReadOnlyDictionary<int, Post>> LoadBatchAsync(IReadOnlyList<int> keys,
                                                                                      CancellationToken cancellationToken)
        {
            using ApiContext context = ContextFactory.CreateDbContext();

            return await context.Posts.Where(p => keys.Contains(p.Id))
                                      .Include(p => p.Tags)
                                      .ToDictionaryAsync(p => p.Id, cancellationToken)
                                      .ConfigureAwait(false);
        }
    }
}
