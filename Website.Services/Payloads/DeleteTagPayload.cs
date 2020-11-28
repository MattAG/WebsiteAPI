using Website.Core.Errors;
using Website.Data.Entities;
using Website.Services.Payloads.Base;

namespace Website.Services.Payloads
{
    /// <summary>
    /// Payload model for deleting tags.
    /// </summary>
    /// <seealso cref="Website.Services.Payloads.Base.TagPayloadBase" />
    public sealed class DeleteTagPayload : TagPayloadBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTagPayload"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public DeleteTagPayload(Tag tag)
            : base(tag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTagPayload"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public DeleteTagPayload(ApiError errors)
            : base(new[] { errors })
        {
        }
    }
}
