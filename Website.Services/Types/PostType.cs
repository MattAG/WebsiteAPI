using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Website.Data.Contexts;
using Website.Data.Entities;
using Website.Services.Data_Loaders;

namespace Website.Services.Types
{
    /// <summary>
    /// Post type mapping.
    /// </summary>
    /// <seealso cref="HotChocolate.Types.ObjectType{Website.Data.Entities.Post}" />
    public sealed class PostType : ObjectType<Post>
    {
        /// <summary>
        /// Configures the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            descriptor.ImplementsNode()
                      .IdField(p => p.Id)
                      .ResolveNode((c, i) => c.DataLoader<PostByIdDataLoader>()
                                              .LoadAsync(i, c.RequestAborted));

            descriptor.Field(p => p.Tags)
                      .ResolveWith<PostResolvers>(p => p.GetTagsAsync(default!, default!, default!, default))
                      .UseDbContext<ApiContext>()
                      .Name("tags");
        }

        /// <summary>
        /// Resolvers for related entities of posts.
        /// </summary>
        private class PostResolvers
        {
            /// <summary>
            /// Gets the tags asynchronous.
            /// </summary>
            /// <param name="post">The post.</param>
            /// <param name="context">The context.</param>
            /// <param name="dataLoader">The data loader.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>List of related tags.</returns>
            public async Task<IEnumerable<Tag>> GetTagsAsync(Post post,
                                                             [ScopedService] ApiContext context,
                                                             TagByIdDataLoader dataLoader,
                                                             CancellationToken cancellationToken)
            {
                int[] tagIds = await context.Posts.AsNoTracking()
                                                  .Where(p => p.Id == post.Id)
                                                  .Include(p => p.Tags)
                                                  .SelectMany(p => p.Tags.Select(t => t.Id))
                                                  .ToArrayAsync(cancellationToken)
                                                  .ConfigureAwait(false);

                return await dataLoader.LoadAsync(tagIds, cancellationToken)
                                       .ConfigureAwait(false);
            }
        }
    }
}
