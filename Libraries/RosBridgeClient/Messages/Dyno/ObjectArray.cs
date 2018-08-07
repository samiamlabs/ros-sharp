/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Messages.Dyno
{
    public class ObjectArray : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "dyno_world_state/Object";

        public Object[] objects;

        public ObjectArray()
        {
            objects = null;
        }
    }
}