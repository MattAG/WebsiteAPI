using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Website.Data.Entities.Base;

namespace Website.Data.Entities
{
    /// <summary>
    /// Post entity model.
    /// </summary>
    /// <seealso cref="Website.Data.Entities.Base.EntityBase" />
    public class Post : EntityBase
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual List<Tag> Tags { get; set; } = new();
    }
}
