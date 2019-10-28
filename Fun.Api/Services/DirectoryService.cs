using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fun.Api.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly Settings _settings;

        public DirectoryService(Settings settings)
        {
            _settings = settings;
        }

        public IEnumerable<string> GetMediaFileNames()
        {
            var files = Directory.GetFiles(_settings.MediaBasePath);
            return files.Select(f => Path.GetFileName(f));
        }
    }
}
