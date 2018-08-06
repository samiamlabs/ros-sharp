/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class DynoClockSync : Publisher<Messages.Rosgraph.Clock>
    {
        public float syncPeriodInSeconds = 5.0f;

        public float seconds;

        private Messages.Rosgraph.Clock clock;
        private float nextSync = 0.0f;

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
            

            if (Time.time > nextSync)
            {
                nextSync = Time.time + syncPeriodInSeconds;
                Publish(clock);
            }
        }

    }
}
