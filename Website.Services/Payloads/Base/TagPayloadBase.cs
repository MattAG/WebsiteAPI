using System.Collections.Generic;
using Website.Core.Errors;
using Website.Data.Entities;

namespace Website.Services.Payloads.Base
{
    /// <summary>
    /// Common base class for tag-related mutation payloads.
    /// </summary>
    /// <seealso cref="Website.Services.Payloads.Base.PayloadBase" />
    public abstract class TagPayloadBase : PayloadBase
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public Tag? Tag { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagPayloadBase"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        protected TagPayloadBase(Tag tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagPayloadBase"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        protected TagPayloadBase(IReadOnlyList<ApiError>? errors = null)
            : base(errors)
        {
        }
    }
}
