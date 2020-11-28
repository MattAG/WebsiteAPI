using System.Collections.Generic;
using Website.Core.Errors;

namespace Website.Services.Payloads.Base
{
    /// <summary>
    /// Common base class for mutation payloads.
    /// </summary>
    public abstract class PayloadBase
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IReadOnlyList<ApiError>? Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadBase"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        protected PayloadBase(IReadOnlyList<ApiError>? errors = null)
        {
            Errors = errors;
        }
    }
}
