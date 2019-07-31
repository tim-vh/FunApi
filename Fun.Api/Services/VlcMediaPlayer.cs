using System.Diagnostics;
using System.IO;

namespace Fun.Api.Services
{
    public class VlcMediaPlayer : IMediaPlayer
    {
        private static Process _currentProcess;
        private readonly Settings _settings;

        public VlcMediaPlayer(Settings settings)
        {
            _settings = settings;
        }

        public void Play(string fileName)
        {
            if (_currentProcess == null || _currentProcess.HasExited)
            {
                var mediaFileFullPath = Path.Combine(_settings.MediaBasePath, fileName);

                _currentProcess = new Process();
                _currentProcess.StartInfo.FileName = _settings.VlcPath;
                _currentProcess.StartInfo.Arguments = $"{mediaFileFullPath} --fullscreen vlc://quit";

                _currentProcess.Start();
            }
        }

        public void Stop()
        {
            if (_currentProcess != null && !_currentProcess.HasExited)
            {
                _currentProcess.Kill();
            }
        }
    }
}
