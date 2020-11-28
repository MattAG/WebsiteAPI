using System.Collections.Generic;
using Website.Core.Errors;
using Website.Data.Entities;

namespace Website.Services.Payloads.Base
{
    /// <summary>
    /// Common base class for post-related mutation payloads.
    /// </summary>
    /// <seealso cref="Website.Services.Payloads.Base.PayloadBase" />
    public abstract class PostPayloadBase : PayloadBase
    {
        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <value>
        /// The post.
        /// </value>
        public Post? Post { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostPayloadBase"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        protected PostPayloadBase(Post post)
        {
            Post = post;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostPayloadBase"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        protected PostPayloadBase(IReadOnlyList<ApiError> errors)
            : base(errors)
        {
        }
    }
}
