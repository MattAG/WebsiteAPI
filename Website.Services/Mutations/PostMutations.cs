using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Website.Core.Errors;
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
    [Authorize]
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
                Created = DateTimeOffset.UtcNow
            };

            if (await context.Posts.FirstOrDefaultAsync(p => p.Title == input.Title)
                                   .ConfigureAwait(false) != null)
            {
                return new AddPostPayload(new ApiError("POST_WITH_TITLE_EXISTS", "A post with that title already exists."));
            }

            post = context.Posts.Add(post).Entity;

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            List<int> tagIds = input.Tags.ConvertAll(t => t.Id);

            post.Tags = await context.Tags.AsNoTracking()
                                          .Where(t => tagIds.Contains(t.Id))
                                          .ToListAsync()
                                          .ConfigureAwait(false);

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

            await ApplyUpdatedValuesToPost(post, input, context).ConfigureAwait(false);

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            await eventSender.SendAsync(nameof(PostSubscriptions.OnPostUpdatedAsync), post.Id)
                             .ConfigureAwait(false);

            return new UpdatePostPayload(post);
        }

        /// <summary>
        /// Applies the updated values to post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        private static async Task ApplyUpdatedValuesToPost(Post post, UpdatePostInput input, ApiContext context)
        {
            post.Title = input.Title;
            post.Content = input.Content;
            post.Modified = DateTimeOffset.UtcNow;

            List<int> updatedTagIds = input.Tags.ConvertAll(t => t.Id);

            post.Tags.RemoveAll(t => !updatedTagIds.Contains(t.Id));

            List<int> currentTagIds = post.Tags.ConvertAll(t => t.Id);

            List<Tag> tagsToAdd = await context.Tags.AsNoTracking()
                                                    .Where(t => updatedTagIds.Contains(t.Id) && !currentTagIds.Contains(t.Id))
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);

            post.Tags.AddRange(tagsToAdd);
        }
    }
}
