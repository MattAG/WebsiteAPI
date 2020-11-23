using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Website.Data.Entities;
using Website.Services.Data_Loaders;

namespace Website.Services.Subscriptions
{
    /// <summary>
    /// Subscriptions for posts.
    /// </summary>
    [ExtendObjectType(Name = "Subscription")]
    public sealed class PostSubscriptions
    {
        /// <summary>
        /// Called when [post updated asynchronous].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataLoader">The data loader.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Updated post.</returns>
        [Subscribe]
        [Topic]
        public Task<Post> OnPostUpdatedAsync([EventMessage] int id,
                                             PostByIdDataLoader dataLoader,
                                             CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}
