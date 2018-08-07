/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Services.Dyno
{
    public class GetObjectsRequest : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "dyno_world_state/GetObjects";
    }

    public class GetObjectsResponse : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "dyno_world_state/GetObjects";
        public Messages.Dyno.Object[] objects;

        public GetObjectsResponse(Messages.Dyno.Object[] objects)
        {
            this.objects = objects;
        }
    }
}
