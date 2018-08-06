/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class DynoTwistSubscriber : Subscriber<Messages.Geometry.Twist>
    {
        public DynoTwistController twistController;

        public float linearVelocityX = 0.0f;
        public float linearVelocityY = 0.0f;
        public float angularVelocity = 0.0f;

        protected override void Start()
        {
            base.Start();
            Application.runInBackground = true;

        }

        protected override void ReceiveMessage(Messages.Geometry.Twist message)
        {
            linearVelocityX = message.linear.x;
            linearVelocityY = message.linear.y;
            angularVelocity = message.angular.z;

            twistController.UpdateMovevent(linearVelocityX, linearVelocityY, angularVelocity);
        }


    }
}
