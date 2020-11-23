using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Website.Data.Contexts;
using Website.Data.Entities;
using Website.Services.Data_Loaders;
using Website.Services.Inputs;
using Website.Services.Types;

namespace Website.Services.Queries
{
    /// <summary>
    /// Queries for posts.
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public sealed class PostQueries
    {
        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of posts.</returns>
        [UseDbContext(typeof(ApiContext))]
        [UsePaging(typeof(NonNullType<PostType>))]
        [UseFiltering(typeof(FilterPostInput))]
        [UseSorting]
        public async Task<List<Post>> GetPosts([ScopedService] ApiContext context,
                                               CancellationToken cancellationToken) => await context.Posts.AsNoTracking()
                                                                                                          .ToListAsync(cancellationToken)
                                                                                                          .ConfigureAwait(false);

        /// <summary>
        /// Gets the post asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataLoader">The data loader.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The post, with the given ID.</returns>
        public async Task<Post?> GetPostByIdAsync([ID(nameof(Post))] int id,
                                                  PostByIdDataLoader dataLoader,
                                                  CancellationToken cancellationToken) => await dataLoader.LoadAsync(id, cancellationToken)
                                                                                                          .ConfigureAwait(false);

        /// <summary>
        /// Gets the post by title asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The post, with the given name.</returns>
        [UseDbContext(typeof(ApiContext))]
        public async Task<Post?> GetPostByTitleAsync(string title,
                                                     [ScopedService] ApiContext context,
                                                     CancellationToken cancellationToken) => await context.Posts.AsNoTracking()
                                                                                                                .FirstOrDefaultAsync(p => p.Title == title, cancellationToken)
                                                                                                                .ConfigureAwait(false);
    }
}
