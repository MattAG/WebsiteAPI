namespace Website.Core.Errors
{
    /// <summary>
    /// Error information container class.
    /// </summary>
    public sealed class ApiError
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ApiError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
