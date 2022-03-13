﻿using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Fun.Api.Repositories.Wwwroot
{
    public class WwwrootVideoRepository : IVideoRepository
    {
        private readonly GetVideosFromWwwrootQuery _getVideosQuery;

        public WwwrootVideoRepository(GetVideosFromWwwrootQuery getVideosQuery)
        {
            _getVideosQuery = getVideosQuery;
        }

        public Video GetVideo(string url)
        {
            return GetVideos().FirstOrDefault(v => v.Url == url);
        }

        public IEnumerable<Video> GetVideos()
        {
            return _getVideosQuery.Execute();
        }
    }
}
