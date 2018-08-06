/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class DynoPoseStampedPublisher : Publisher<Messages.Geometry.PoseStamped>
    {
        private Messages.Geometry.PoseStamped message;
        public string FrameId = "Unity";

        public ClockSubscriber m_SimulationTime;

        public Transform PublishedTransform;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
                UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Geometry.PoseStamped();
            message.header = new Messages.Standard.Header();
            message.header.frame_id = FrameId;
        }

        private void UpdateMessage()
        {
            message.header.Update();

            if(m_SimulationTime != null)
            {
                message.header.stamp.secs = m_SimulationTime.adjustedSeconds;
                message.header.stamp.nsecs = m_SimulationTime.adjustedNanoSeconds;
            }

            message.pose.position = GetGeometryPoint(PublishedTransform.position.Unity2Ros());
            message.pose.orientation = GetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros());

            Publish(message);
        }

        private Messages.Geometry.Point GetGeometryPoint(Vector3 position)
        {
            Messages.Geometry.Point geometryPoint = new Messages.Geometry.Point();
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
            return geometryPoint;
        }

        private Messages.Geometry.Quaternion GetGeometryQuaternion(Quaternion quaternion)
        {
            Messages.Geometry.Quaternion geometryQuaternion = new Messages.Geometry.Quaternion();
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
            return geometryQuaternion;
        }
    }
}
