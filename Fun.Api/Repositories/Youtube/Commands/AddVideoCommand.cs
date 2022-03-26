using Fun.Api.Repositories.Youtube.Model;
using Provocq;

namespace Fun.Api.Repositories.Youtube.Commands
{
    public class AddVideoCommand : ICommand<YoutubeVideoDataContext>
    {
        public YoutubeVideo YoutubeVideo { get; set; }

        public void Execute(YoutubeVideoDataContext context)
        {
            context.Videos.Add(YoutubeVideo);
        }
    }
}
