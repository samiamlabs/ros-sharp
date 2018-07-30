/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient.Messages.Sensor;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{

    public class ForkliftSubscriber : Subscriber<JointState>
    {

        public ForkliftController forkliftController;

        protected override void Start()
        {
            base.Start();
        }


        protected override void ReceiveMessage(JointState message)
        {
            for (int i = 0; i < message.name.Length; i++)
            {
                if (message.name[i] == "drive_wheel_joint")
                {
                    forkliftController.driveWheelAngularVelocity = message.velocity[i];
                }
                else if (message.name[i] == "steer_joint")
                {
                    forkliftController.steerAngle = -message.position[i];
                }
                else if (message.name[i] == "fork_joint")
                {
                    forkliftController.forkPosition = message.position[i];
                }
            }
        }


    }

}
