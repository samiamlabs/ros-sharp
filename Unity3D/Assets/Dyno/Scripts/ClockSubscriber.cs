/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using System.Collections.Generic;
using RosSharp.RosBridgeClient.Messages.Rosgraph;

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class ClockSubscriber : Subscriber<Messages.Rosgraph.Clock> {
        public int seconds;
        public int nanoSeconds;

        public int roundtripDelayNanoSeconds = 100000;

        public int adjustedSeconds;
        public int adjustedNanoSeconds;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Clock message)
        {
            seconds = message.clock.secs;
            nanoSeconds = message.clock.nsecs;

            if (roundtripDelayNanoSeconds > nanoSeconds)
            {
                adjustedSeconds = seconds - 1;
                adjustedNanoSeconds = 1000000000 - roundtripDelayNanoSeconds;
            } else
            {
                adjustedSeconds = seconds;
                adjustedNanoSeconds = nanoSeconds - roundtripDelayNanoSeconds;
            }

            if (adjustedNanoSeconds < 0)
            {
                Debug.Log(adjustedNanoSeconds);
            }
        }
    }

}
