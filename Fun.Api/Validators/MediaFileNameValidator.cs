using Fun.Api.Services;
using System;
using System.Linq;

namespace Fun.Api.Validators
{
    public class MediaFileNameValidator : IMediaFileNameValidator
    {
        private readonly IDirectoryService _directoryService;

        public MediaFileNameValidator(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public bool Validate(string fileName)
        {
            var fileNames = _directoryService.GetMediaFileNames();
            return fileNames.Contains(fileName);
        }
    }
}
