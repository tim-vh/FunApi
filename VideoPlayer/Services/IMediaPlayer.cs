namespace Fun.VideoPlayer.Services
{
    public interface IMediaPlayer
    {
        void Play(string uri);
        void Stop();
    }
}
