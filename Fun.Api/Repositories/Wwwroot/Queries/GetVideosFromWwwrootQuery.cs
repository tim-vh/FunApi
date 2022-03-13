using Fun.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fun.Api.Repositories.Wwwroot.Queries
{
    public class GetVideosFromWwwrootQuery
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetVideosFromWwwrootQuery(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Video> Execute()
        {
            var rootFolder = _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var test = WebHostDefaults.WebRootKey;
            string[] files = GetFiles(rootFolder);

            var baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;

            return files.Select(f => new Video
            {
                Url = $"{baseUrl}/videos/{Path.GetFileName(f)}",
                Thumbnail = $"{baseUrl}/videos/{Path.GetFileName(f)}.jpeg",
                Name = Path.GetFileNameWithoutExtension(f)
                        .Replace("_", " ")
                        .Replace("-", " ")
                        .Trim()
            });
        }

        private static string[] GetFiles(string rootFolder)
        {
            var path = Path.Combine(rootFolder, "wwwroot/videos");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Directory.GetFiles(path, "*.mp4");
        }
    }
}