using HotChocolate.Data.Filters;
using Website.Data.Entities;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for filtering post queries.
    /// </summary>
    /// <seealso cref="HotChocolate.Data.Filters.FilterInputType{Website.Data.Entities.Post}" />
    public sealed class FilterPostInput : FilterInputType<Post>
    {
        /// <summary>
        /// Configures the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IFilterInputTypeDescriptor<Post> descriptor)
        {
            descriptor.Ignore(p => p.Id);
            descriptor.Ignore(p => p.Tags);
        }
    }
}
