using Fun.Api.DataModel;
using Fun.Api.Services;
using Provocq;
using System.IO;
using System.Linq;

namespace Fun.Api.Persistance
{
    // TODO: remove
    public class InitializingJsonPersistor : JsonFilePersistor<FunDataContext>
    {
        private const string _filePath = "appdata/appdata.json";

        public InitializingJsonPersistor(IGetVideosQuery query) : base(_filePath)
        {
            if (!File.Exists(_filePath))
            {
                var context = new FunDataContext
                {
                    Videos = query.Execute().ToList()
                };

                Save(context);
            }
        }
    }
}
