using HotChocolate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Website.Data.Contexts;
using Website.Services.Data_Loaders;
using Website.Services.Mutations;
using Website.Services.Queries;
using Website.Services.Subscriptions;
using Website.Services.Types;

namespace Website.API
{
    /// <summary>
    /// Handles the configuration of the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApiContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DevelopmentDatabase")));

            services.AddAuthorization();

            services.AddGraphQLServer()
                    .AddQueryType(t => t.Name("Query"))
                        .AddTypeExtension<PostQueries>()
                        .AddTypeExtension<TagQueries>()
                    .AddMutationType(t => t.Name("Mutation"))
                        .AddTypeExtension<PostMutations>()
                        .AddTypeExtension<TagMutations>()
                    .AddSubscriptionType(t => t.Name("Subscription"))
                        .AddTypeExtension<PostSubscriptions>()
                    .AddType<PostType>()
                    .AddType<TagType>()
                    .EnableRelaySupport()
                    .AddFiltering()
                    .AddSorting()
                    .AddInMemorySubscriptions()
                    .AddDataLoader<PostByIdDataLoader>()
                    .AddDataLoader<TagByIdDataLoader>();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseWebSockets()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints.MapGraphQL());
        }
    }
}
