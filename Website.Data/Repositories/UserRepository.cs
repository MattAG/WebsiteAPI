using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website.Data.Contexts;
using Website.Data.Entities;

namespace Website.Data.Repositories
{
    /// <summary>
    /// Repository for handling users.
    /// </summary>
    public sealed class UserRepository
    {
        /// <summary>
        /// Gets the context factory.
        /// </summary>
        /// <value>
        /// The context factory.
        /// </value>
        private IDbContextFactory<ApiContext> ContextFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public UserRepository(IDbContextFactory<ApiContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        /// <summary>
        /// Gets the by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User with provided username.</returns>
        public async Task<User?> GetByUsername(string username)
        {
            using ApiContext context = ContextFactory.CreateDbContext();

            return await context.Users.SingleOrDefaultAsync(u => u.Username == username)
                                      .ConfigureAwait(false);
        }
    }
}
