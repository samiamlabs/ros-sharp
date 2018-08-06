/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class DynoClockPublisher : Publisher<Messages.Rosgraph.Clock>
    {
        public float seconds;
        private Messages.Rosgraph.Clock clock;

        protected override void Start()
        {
            base.Start();
            clock = new Messages.Rosgraph.Clock();
        }

        private void FixedUpdate()
        {
            // Use Standard Header Extension as source of truth for time
            Messages.Standard.Header header = new Messages.Standard.Header();
            header.Update();
            clock.clock = header.stamp;

            seconds = (float)header.stamp.secs;

            Publish(clock);
        }

    }
}
