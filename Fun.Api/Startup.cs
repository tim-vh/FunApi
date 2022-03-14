using Fun.Api.Helpers;
using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot;
using Fun.Api.Repositories.Wwwroot.Queries;
using Fun.Api.Repositories.Youtube;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fun.Api
{
    public class Startup
    {
        private readonly string _funApiCorsPolicy = "FunApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(_funApiCorsPolicy, builder =>
            {
                builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            services.AddRazorPages();
            services.AddMvc(options => options.Filters.Add(typeof(ExceptionLogger)));
            services.AddSingleton(Configuration);
            services.AddScoped<IVideoCatalog, VideoCatalog>();
            services.AddScoped<IVideoRepository, WwwrootVideoRepository>();
            services.AddScoped<IVideoRepository, YoutubeVideoRepository>();
            services.AddScoped<GetVideosFromWwwrootQuery, GetVideosFromWwwrootQuery>();
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_funApiCorsPolicy);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<VideoHub>("/videohub");
            });
        }
    }
}