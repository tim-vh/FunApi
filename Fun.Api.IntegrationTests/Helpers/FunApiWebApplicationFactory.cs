using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Fun.Api.IntegrationTests.Helpers
{
    public class FunApiWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var config = new List<KeyValuePair<string, string>>();
            config.Add(KeyValuePair.Create("youtubeVideosFile", Path.GetFullPath("TestData/youtubevideos.json")));

            builder.ConfigureAppConfiguration(test => test.AddInMemoryCollection(config));
        }
    }
}
