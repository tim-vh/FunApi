using Fun.Api.DataModel;
using Fun.Api.Helpers;
using Fun.Api.Persistance;
using Fun.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Provocq;

namespace Fun.Api
{
    public class Startup
    {
        private readonly string FunApiCorsPolicy = "FunApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(FunApiCorsPolicy, builder =>
            {
                builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            services.AddRazorPages();
            services.AddMvc(options => options.Filters.Add(typeof(ExceptionLogger)));
            services.AddSingleton(Configuration);
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddTransient<IGetVideosQuery, GetVideosQuery>(); // TODO: remove

            services.AddSingleton<IPersistor<FunDataContext>, InitializingJsonPersistor>();
            services.AddSingleton<BlockingDataHandler<FunDataContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(FunApiCorsPolicy);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<VideoHub>("/videohub");
            });
        }
    }
}