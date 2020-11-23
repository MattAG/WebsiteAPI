using System.Collections.Generic;
using Website.Data.Entities;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for creating posts.
    /// </summary>
    public record AddPostInput(string Title,
                               string Content,
                               List<Tag> Tags);
}
