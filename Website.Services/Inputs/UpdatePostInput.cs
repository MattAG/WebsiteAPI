using System.Collections.Generic;
using HotChocolate.Types.Relay;
using Website.Data.Entities;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for updating posts.
    /// </summary>
    public record UpdatePostInput([ID(nameof(Post))] int Id,
                                  string Title,
                                  string Content,
                                  List<PostTagInput> Tags);
}
