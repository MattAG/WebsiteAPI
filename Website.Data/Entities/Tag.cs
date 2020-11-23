using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Website.Data.Entities.Base;

namespace Website.Data.Entities
{
    /// <summary>
    /// Tag entity model.
    /// </summary>
    /// <seealso cref="Website.Data.Entities.Base.EntityBase" />
    public class Tag : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        /// <value>
        /// The posts.
        /// </value>
        public List<Post> Posts { get; set; } = new();
    }
}
