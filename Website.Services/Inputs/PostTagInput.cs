using HotChocolate.Types.Relay;
using Website.Data.Entities;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for the related tags, when adding or updating a post.
    /// </summary>
    public record PostTagInput([ID(nameof(Tag))] int Id);
}
