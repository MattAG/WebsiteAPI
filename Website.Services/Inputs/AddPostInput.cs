using System.Collections.Generic;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for creating posts.
    /// </summary>
    public record AddPostInput(string Title,
                               string Content,
                               List<PostTagInput> Tags);
}
