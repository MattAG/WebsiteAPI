using System.Collections.Generic;
using Website.Core.Errors;
using Website.Data.Entities;
using Website.Services.Payloads.Base;

namespace Website.Services.Payloads
{
    /// <summary>
    /// Payload model for creating tags.
    /// </summary>
    /// <seealso cref="Website.Services.Payloads.Base.TagPayloadBase" />
    public sealed class AddTagPayload : TagPayloadBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTagPayload"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public AddTagPayload(Tag tag)
            : base(tag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTagPayload"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public AddTagPayload(IReadOnlyList<ApiError>? errors = null)
            : base(errors)
        {
        }
    }
}
