using Fun.Api.Model;
using System.Collections.Generic;

namespace Fun.Api.Services
{
    public interface IGetVideosQuery
    {
        IEnumerable<Video> Execute();
    }
}