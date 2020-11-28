using Website.Core.Errors;
using Website.Data.Entities;
using Website.Services.Payloads.Base;

namespace Website.Services.Payloads
{
    /// <summary>
    /// Payload model for updating posts.
    /// </summary>
    /// <seealso cref="Website.Services.Payloads.Base.PostPayloadBase" />
    public sealed class UpdatePostPayload : PostPayloadBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePostPayload"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        public UpdatePostPayload(Post post)
            : base(post)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePostPayload"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public UpdatePostPayload(ApiError error)
            : base(new[] { error })
        {
        }
    }
}
