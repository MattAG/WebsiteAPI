using HotChocolate.Data.Filters;
using Website.Data.Entities;

namespace Website.Services.Inputs
{
    /// <summary>
    /// Input model for filtering tag queries.
    /// </summary>
    /// <seealso cref="HotChocolate.Data.Filters.FilterInputType{Website.Data.Entities.Tag}" />
    public sealed class FilterTagInput : FilterInputType<Tag>
    {
        /// <summary>
        /// Configures the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IFilterInputTypeDescriptor<Tag> descriptor)
        {
            descriptor.Ignore(p => p.Id);
            descriptor.Ignore(p => p.Posts);
        }
    }
}
