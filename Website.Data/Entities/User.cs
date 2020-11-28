using System.ComponentModel.DataAnnotations;

namespace Website.Data.Entities
{
    /// <summary>
    /// User entity model.
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Key]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; } = string.Empty;
    }
}
