using Fun.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fun.Api.Services
{
    public class GetVideosQuery : IGetVideosQuery
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetVideosQuery(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Video> Execute()
        {
            var rootFolder = _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var test = WebHostDefaults.WebRootKey;
            var files = Directory.GetFiles(Path.Combine(rootFolder, "wwwroot/videos"), "*.mp4");
            var baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;

            return files.Select(f => new Video
            {
                Url = $"{baseUrl}/videos/{Path.GetFileName(f)}",
                Filename = Path.GetFileName(f),
                Thumbnail = $"{baseUrl}/videos/{Path.GetFileName(f)}.jpeg",
                Name = Path.GetFileNameWithoutExtension(f)
                        .Replace("_", " ")
                        .Replace("-", " ")
                        .Trim()
            });
        }
    }
}