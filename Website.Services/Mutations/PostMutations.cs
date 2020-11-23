using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Website.Core.Classes;
using Website.Data.Contexts;
using Website.Data.Entities;
using Website.Services.Inputs;
using Website.Services.Payloads;
using Website.Services.Subscriptions;

namespace Website.Services.Mutations
{
    /// <summary>
    /// Mutations for posts.
    /// </summary>
    [ExtendObjectType(Name = "Mutation")]
    public sealed class PostMutations
    {
        /// <summary>
        /// Adds the post asynchronous.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>Add post payload.</returns>
        [UseDbContext(typeof(ApiContext))]
        public async Task<AddPostPayload> AddPostAsync(AddPostInput input,
                                                       [ScopedService] ApiContext context)
        {
            Post post = new()
            {
                Title = input.Title,
                Content = input.Content,
                Created = DateTimeOffset.UtcNow,
                Tags = input.Tags
            };

            if (await context.Posts.FirstOrDefaultAsync(p => p.Title == input.Title)
                                   .ConfigureAwait(false) != null)
            {
                return new AddPostPayload(new ApiError("POST_WITH_TITLE_EXISTS", "A post with that title already exists."));
            }

            post = context.Posts.Add(post).Entity;

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            return new AddPostPayload(post);
        }

        /// <summary>
        /// Updates the post asynchronous.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <param name="eventSender">The event sender.</param>
        /// <returns>Update post payload.</returns>
        [UseDbContext(typeof(ApiContext))]
        public async Task<UpdatePostPayload> UpdatePostAsync(UpdatePostInput input,
                                                             [ScopedService] ApiContext context,
                                                             [Service] ITopicEventSender eventSender)
        {
            Post post = await context.Posts.Include(p => p.Tags)
                                           .SingleOrDefaultAsync(p => p.Id == input.Id)
                                           .ConfigureAwait(false);

            if (post == null)
            {
                return new UpdatePostPayload(new ApiError("POST_NOT_FOUND", "Post not found."));
            }

            Post postWithTitle = await context.Posts.FirstOrDefaultAsync(p => p.Title == input.Title)
                                                    .ConfigureAwait(false);

            if (postWithTitle != null && postWithTitle.Id != post.Id)
            {
                return new UpdatePostPayload(new ApiError("POST_WITH_TITLE_EXISTS", "A post with that title already exists."));
            }

            post.Title = input.Title;
            post.Content = input.Content;
            post.Modified = DateTimeOffset.UtcNow;

            post.Tags.RemoveAll(t => !input.Tags.Any(it => it.Id == t.Id));
            post.Tags.AddRange(input.Tags.Where(t => !post.Tags.Any(pt => pt.Id == t.Id))
                                         .ToList());

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            await eventSender.SendAsync(nameof(PostSubscriptions.OnPostUpdatedAsync), post.Id)
                             .ConfigureAwait(false);

            return new UpdatePostPayload(post);
        }
    }
}
