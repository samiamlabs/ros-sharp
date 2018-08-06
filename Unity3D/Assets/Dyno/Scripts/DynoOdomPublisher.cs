/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class DynoOdomPublisher : Publisher<Messages.Navigation.Odometry>
    {
        public Rigidbody baseRigidbody;
        public ClockSubscriber simulationTime;

        private Messages.Navigation.Odometry odomMessage;

        private float[] poseCovarianceDiagonal = { 0.001f, 0.001f, 0.001f, 0.001f, 0.001f, 0.03f };
        private float[] twistCovarianceDiagonal = { 0.001f, 0.001f, 0.001f, 0.001f, 0.001f, 0.03f };


        protected override void Start()
        {
            base.Start();
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

        private float[] CovarianceMatrixFromDiagonal(float[] diagonal)
        {
            float[] covarianceMatrix = new float[6 * 6];
            covarianceMatrix[0] = diagonal[0];
            covarianceMatrix[7] = diagonal[1];
            covarianceMatrix[14] = diagonal[2];
            covarianceMatrix[21] = diagonal[3];
            covarianceMatrix[28] = diagonal[4];
            covarianceMatrix[35] = diagonal[5];

            return covarianceMatrix;
        }

        private void FixedUpdate()
        {
            Messages.Geometry.PoseWithCovariance robot_pose = new Messages.Geometry.PoseWithCovariance();
            robot_pose.pose.position = GetGeometryPoint(baseRigidbody.transform.position.Unity2Ros());
            robot_pose.pose.orientation = GetGeometryQuaternion(baseRigidbody.rotation.Unity2Ros());

            robot_pose.covariance = CovarianceMatrixFromDiagonal(poseCovarianceDiagonal);

            Messages.Geometry.TwistWithCovariance twist = new Messages.Geometry.TwistWithCovariance();
            Vector3 localVelocity = baseRigidbody.transform.InverseTransformDirection(baseRigidbody.velocity).Unity2Ros();
            twist.twist.linear.x = localVelocity.x;
            twist.twist.linear.y = localVelocity.y;
            twist.twist.linear.z = localVelocity.z;

            Vector3 localAngularVelocity = baseRigidbody.transform.InverseTransformDirection(baseRigidbody.angularVelocity).Unity2Ros();
            twist.twist.angular.x = localAngularVelocity.x;
            twist.twist.angular.y = localAngularVelocity.y;
            twist.twist.angular.z = localAngularVelocity.z;

            twist.covariance = CovarianceMatrixFromDiagonal(twistCovarianceDiagonal);

            odomMessage = new Messages.Navigation.Odometry { twist=twist, pose=robot_pose };
            odomMessage.header.Update();

            if(simulationTime != null)
            {
                odomMessage.header.stamp.secs = simulationTime.adjustedSeconds;
                odomMessage.header.stamp.nsecs = simulationTime.adjustedNanoSeconds;
            }

            odomMessage.header.frame_id = "odom";
            odomMessage.child_frame_id = "base_footprint";

            Publish(odomMessage);
        }


    }
}
