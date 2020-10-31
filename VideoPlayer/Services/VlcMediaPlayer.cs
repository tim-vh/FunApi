using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fun.VideoPlayer.Services
{
    public class VlcMediaPlayer : IMediaPlayer
    {
        private static Process _currentProcess;
        private readonly Settings _settings;

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        public VlcMediaPlayer(Settings settings)
        {
            _settings = settings;
        }

        public void Play(string uri)
        {
            if (_currentProcess == null || _currentProcess.HasExited)
            {
                _currentProcess = new Process();
                _currentProcess.StartInfo.FileName = _settings.VlcPath;
                _currentProcess.StartInfo.Arguments = $"{uri} --fullscreen vlc://quit";

                _currentProcess.Start();

                SetForegroundWindow(_currentProcess.Handle);
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
