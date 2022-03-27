using Fun.Api;
using Fun.Api.DataModel;
using Fun.Api.Helpers;
using Fun.Api.Identity;
using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot;
using Fun.Api.Repositories.Wwwroot.Queries;
using Fun.Api.Repositories.Youtube;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Provocq;
using System;
using System.Text;

byte[] Key = Encoding.UTF8.GetBytes("Test123Test123Test123Test123Test123Test123Test123");
string _funApiCorsPolicy = "FunApiCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();
ConfigureApp(app);

void ConfigureServices(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            });

    services.AddTransient<IUserStore<ApplicationUser>, FunUserStore>();
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
                .WithMethods("GET")
                .AllowAnyHeader();
    }));

    services.AddRazorPages();
    services.AddMvc(options => options.Filters.Add(typeof(ExceptionLogger)));
    services.AddScoped<IVideoCatalog, VideoCatalog>();
    services.AddScoped<IVideoRepository, WwwrootVideoRepository>();
    services.AddScoped<IVideoRepository, YoutubeVideoRepository>();
    services.AddScoped<GetVideosFromWwwrootQuery, GetVideosFromWwwrootQuery>();

    services.AddTransient<IPersistor<IdentityDataContext>>(_ => new JsonFilePersistor<IdentityDataContext>(builder.Configuration.GetValue<string>("userDataFile")));
    services.AddSingleton<BlockingDataHandler<IdentityDataContext>>();

    services.AddTransient<IPersistor<YoutubeVideoDataContext>>(_ => new JsonFilePersistor<YoutubeVideoDataContext>(builder.Configuration.GetValue<string>("youtubeVideosFile")));
    services.AddSingleton<BlockingDataHandler<YoutubeVideoDataContext>>();

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

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
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

    app.Run();
}

public partial class Program
{
}