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
    /// Data loader for tags, using their ID.
    /// </summary>
    /// <seealso cref="HotChocolate.DataLoader.BatchDataLoader{int, Website.Data.Entities.Tag}" />
    public sealed class TagByIdDataLoader : BatchDataLoader<int, Tag>
    {
        /// <summary>
        /// Gets the context factory.
        /// </summary>
        /// <value>
        /// The context factory.
        /// </value>
        private IDbContextFactory<ApiContext> ContextFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagByIdDataLoader"/> class.
        /// </summary>
        /// <param name="batchScheduler">The batch scheduler.</param>
        /// <param name="contextFactory">The context factory.</param>
        public TagByIdDataLoader(IBatchScheduler batchScheduler,
                                 IDbContextFactory<ApiContext> contextFactory)
            : base(batchScheduler)
        {
            ContextFactory = contextFactory;
        }

        /// <summary>
        /// Loads the batch asynchronous.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Dictionary of tags.</returns>
        protected async override Task<IReadOnlyDictionary<int, Tag>> LoadBatchAsync(IReadOnlyList<int> keys,
                                                                                    CancellationToken cancellationToken)
        {
            using ApiContext context = ContextFactory.CreateDbContext();

            return await context.Tags.Where(t => keys.Contains(t.Id))
                                     .Include(p => p.Posts)
                                     .ToDictionaryAsync(t => t.Id, cancellationToken)
                                     .ConfigureAwait(false);
        }
    }
}
