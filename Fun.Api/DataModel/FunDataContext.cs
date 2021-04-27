using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fun.Api.DataModel
{
    public class FunDataContext
    {
        public FunDataContext()
        {
            Videos = new Collection<Video>();
        }

        public int Version { get { return 1; } }

        public ICollection<Video> Videos { get; set; }
    }
}
