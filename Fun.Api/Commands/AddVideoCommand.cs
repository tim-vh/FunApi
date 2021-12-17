using Fun.Api.DataModel;
using Provocq;

namespace Fun.Api.Commands
{
    public class AddVideoCommand : ICommand<FunDataContext>
    {
        public AddVideoCommand(ApiModel.Video video)
        {
            Video = video;
        }

        public ApiModel.Video Video { get; }

        public void Execute(FunDataContext context)
        {
            context.Videos.Add(new Video
            {
                Name = Video.Name,
                Thumbnail = Video.Thumbnail,
                Url = Video.Url
            });
        }
    }
}