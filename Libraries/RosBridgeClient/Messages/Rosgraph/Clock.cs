/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Messages.Rosgraph
{
    public class Clock: Message
    {
        [JsonIgnore]
        public const string RosMessageName = "rosgraph_msgs/Clock";
        public Standard.Time clock;

        public Clock()
        {
            clock = new Standard.Time();
        }
    }
}
