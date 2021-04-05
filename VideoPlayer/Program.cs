using Fun.VideoPlayer.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fun.VideoPlayer
{
    class Program
    {
        private static IMediaPlayer _mediaPlayer;
        private static Settings _settings;

        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            _settings = configuration.GetSection("FunApi").Get<Settings>();
            _mediaPlayer = new VlcMediaPlayer(_settings);

            var connection = new HubConnectionBuilder()
                .WithUrl(new Uri($"{_settings.ServerAddress}/videohub"))
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();
            Console.WriteLine($"Connected to {_settings.ServerAddress}");

            connection.On<string>("PlayVideo", PlayVideo);
            connection.On("StopVideo", StopVideo);

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();

            await connection.StopAsync();
            Console.WriteLine("Connection closed");
        }

        private static void PlayVideo(string url)
        {
            _mediaPlayer.Play(url);
        }

        private static void StopVideo()
        {
            _mediaPlayer.Stop();
        }
    }
}
