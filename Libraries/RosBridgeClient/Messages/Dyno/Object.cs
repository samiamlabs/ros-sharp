/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Messages.Dyno
{
    public class Object : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "dyno_world_state/Object";
        public string name;
        public Geometry.Pose pose;

        public Object()
        {
            name = "";
            pose = new Geometry.Pose();
        }
    }
}