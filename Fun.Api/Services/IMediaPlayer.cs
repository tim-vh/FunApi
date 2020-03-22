namespace Fun.Api.Services
{
    // todo create a vlc media player client and remove mediaplayer from api
    public interface IMediaPlayer
    {
        void Play(string fileName);
        void Stop();
    }
}
