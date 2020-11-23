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
    /// Tag type mapping.
    /// </summary>
    /// <seealso cref="HotChocolate.Types.ObjectType{Website.Data.Entities.Tag}" />
    public sealed class TagType : ObjectType<Tag>
    {
        /// <summary>
        /// Configures the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Tag> descriptor)
        {
            descriptor.ImplementsNode()
                      .IdField(t => t.Id)
                      .ResolveNode((c, i) => c.DataLoader<TagByIdDataLoader>()
                                              .LoadAsync(i, c.RequestAborted));

            descriptor.Field(t => t.Posts)
                      .ResolveWith<TagResolvers>(t => t.GetPostsAsync(default!, default!, default!, default))
                      .UseDbContext<ApiContext>()
                      .Name("posts");
        }

        /// <summary>
        /// Resolvers for related entities of tags.
        /// </summary>
        private class TagResolvers
        {
            /// <summary>
            /// Gets the posts asynchronous.
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="context">The context.</param>
            /// <param name="dataLoader">The data loader.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>List of related posts.</returns>
            public async Task<IEnumerable<Post>> GetPostsAsync(Tag tag,
                                                               [ScopedService] ApiContext context,
                                                               PostByIdDataLoader dataLoader,
                                                               CancellationToken cancellationToken)
            {
                int[] postIds = await context.Tags.AsNoTracking()
                                                  .Where(t => t.Id == tag.Id)
                                                  .Include(t => t.Posts)
                                                  .SelectMany(t => t.Posts.Select(p => p.Id))
                                                  .ToArrayAsync(cancellationToken)
                                                  .ConfigureAwait(false);

                return await dataLoader.LoadAsync(postIds, cancellationToken)
                                       .ConfigureAwait(false);
            }
        }
    }
}
