using System.Collections.Generic;

namespace Fun.Api.Services
{
    public interface IDirectoryService
    {
        IEnumerable<string> GetMediaFileNames();
    }
}