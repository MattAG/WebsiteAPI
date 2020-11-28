using Website.Core.Errors;
using Website.Data.Entities;
using Website.Services.Payloads.Base;

namespace Website.Services.Payloads
{
    /// <summary>
    /// Payload model for creating posts.
    /// </summary>
    public sealed class AddPostPayload : PostPayloadBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPostPayload"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        public AddPostPayload(Post post)
            : base(post)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPostPayload"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public AddPostPayload(ApiError error)
            : base(new[] { error })
        {
        }
    }
}
