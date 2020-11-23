using Microsoft.EntityFrameworkCore;
using Website.Data.Entities;

namespace Website.Data.Contexts
{
    /// <summary>
    /// Database context for the API.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public sealed class ApiContext : DbContext
    {
        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        /// <value>
        /// The posts.
        /// </value>
        public DbSet<Post> Posts { get; set; } = default!;

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public DbSet<Tag> Tags { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public ApiContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
