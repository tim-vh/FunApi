using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fun.Api.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IConfiguration _configuration;

        public DirectoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<string> GetMediaFileNames()
        {
            var rootFolder = _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var files = Directory.GetFiles(Path.Combine(rootFolder, "wwwroot/videos"));
            return files.Select(f => Path.GetFileName(f));
        }
    }
}
