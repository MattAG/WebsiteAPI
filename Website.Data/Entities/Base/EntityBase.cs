using System.ComponentModel.DataAnnotations;

namespace Website.Data.Entities.Base
{
    /// <summary>
    /// Common base class for entity models.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }
    }
}
