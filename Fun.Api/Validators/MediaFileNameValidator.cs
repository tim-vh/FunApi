using System;
using System.IO;
using System.Linq;

namespace Fun.Api.Validators
{
    public class MediaFileNameValidator : IMediaFileNameValidator
    {
        private readonly Settings _settings;

        public MediaFileNameValidator(Settings settings)
        {
            _settings = settings;
        }

        public bool Validate(string fileName)
        {
            var files = Directory.GetFiles(_settings.MediaBasePath);
            var fileNames = files.Select(f => Path.GetFileName(f));
            return fileNames.Contains(fileName);
        }
    }
}
