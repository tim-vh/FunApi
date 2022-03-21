using Fun.Api.Helpers;
using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot;
using Fun.Api.Repositories.Wwwroot.Queries;
using Fun.Api.Repositories.Youtube;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Linq;
using System.Text;


namespace Fun.Api
{
    public class Startup
    {
        public static byte[] Key = Encoding.UTF8.GetBytes("Test123Test123Test123Test123Test123Test123Test123");
        private readonly string _funApiCorsPolicy = "FunApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            });

            services.AddTransient<IUserStore<IdentityUser>, FunUserStore>();
            services.AddTransient<IRoleStore<IdentityRole>, FunUserStore>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

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
            
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "Type into the textbox: {your JWT token}."
                });

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });            
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