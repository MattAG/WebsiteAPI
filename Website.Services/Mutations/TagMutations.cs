using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Website.Core.Classes;
using Website.Data.Contexts;
using Website.Data.Entities;
using Website.Services.Inputs;
using Website.Services.Payloads;

namespace Website.Services.Mutations
{
    /// <summary>
    /// Mutations for tags.
    /// </summary>
    [ExtendObjectType(Name = "Mutation")]
    public sealed class TagMutations
    {
        /// <summary>
        /// Adds the tag asynchronous.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>Add tag payload.</returns>
        [UseDbContext(typeof(ApiContext))]
        public async Task<AddTagPayload> AddTagAsync(AddTagInput input,
                                                     [ScopedService] ApiContext context)
        {
            Tag tag = new()
            {
                Name = input.Name
            };

            tag = context.Tags.Add(tag).Entity;

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            return new AddTagPayload(tag);
        }

        /// <summary>
        /// Deletes the tag asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>Delete tag payload.</returns>
        [UseDbContext(typeof(ApiContext))]
        public async Task<DeleteTagPayload> DeleteTagAsync([ID(nameof(Tag))] int id,
                                                           [ScopedService] ApiContext context)
        {
            Tag tag = await context.Tags.FindAsync(id)
                                        .ConfigureAwait(false);

            if (tag == null)
            {
                return new DeleteTagPayload(new ApiError("TAG_NOT_FOUND", "Tag not found."));
            }

            context.Tags.Remove(tag);

            await context.SaveChangesAsync()
                         .ConfigureAwait(false);

            return new DeleteTagPayload(tag);
        }
    }
}
