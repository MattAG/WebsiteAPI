using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Website.Data.Contexts;
using Website.Data.Entities;
using Website.Services.Inputs;
using Website.Services.Types;

namespace Website.Services.Queries
{
    /// <summary>
    /// Queries for tags.
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public sealed class TagQueries
    {
        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>List of tags.</returns>
        [UseDbContext(typeof(ApiContext))]
        [UsePaging(typeof(NonNullType<TagType>))]
        [UseFiltering(typeof(FilterTagInput))]
        [UseSorting]
        public async Task<List<Tag>> GetTags([ScopedService] ApiContext context) => await context.Tags.AsNoTracking()
                                                                                                      .ToListAsync()
                                                                                                      .ConfigureAwait(false);
    }
}
