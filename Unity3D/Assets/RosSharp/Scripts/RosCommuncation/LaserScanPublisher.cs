/*
© Siemens AG, 2017-2018
Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaserScanPublisher : Publisher<Messages.Sensor.LaserScan>
    {
        private Messages.Sensor.LaserScan message;
        public string FrameId = "base_scan_link";

        public ClockSubscriber m_SimulationTime;

        public LaserScanReader m_LaserScanReader;

        private int m_ShouldPublishCounter;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();

            if (m_LaserScanReader != null)
            {
                ResetShouldPublishCounter();
            }
        }

        private void ResetShouldPublishCounter()
        {
            m_ShouldPublishCounter = -1 + (int)(1 / Time.fixedDeltaTime) / m_LaserScanReader.m_ScanningFrequency;
        }

        private void FixedUpdate()
        {
            if (m_LaserScanReader != null)
            {
                if (m_ShouldPublishCounter == 0)
                {
                    UpdateMessage();
                    ResetShouldPublishCounter();
                }
                else
                {
                    m_ShouldPublishCounter--;
                }
            }
            else
            {
                UpdateMessage();
            }
        }

        private void InitializeMessage()
        {
            if (m_LaserScanReader != null)
            {
                int numLines = (int)Mathf.Round(m_LaserScanReader.m_ApertureAngle / m_LaserScanReader.m_AngularResolution) + 1;

                float timeIncrement = 0.0f;
                if (m_LaserScanReader.m_UseTimeIncrement)
                {
                    timeIncrement = 1 / ((float)m_LaserScanReader.m_ScanningFrequency * (float)numLines);
                }

                message = new Messages.Sensor.LaserScan
                {
                    header = new Messages.Standard.Header { frame_id = FrameId },
                    angle_min = Mathf.Deg2Rad * -m_LaserScanReader.m_ApertureAngle / 2,
                    angle_max = Mathf.Deg2Rad * m_LaserScanReader.m_ApertureAngle / 2,
                    angle_increment = Mathf.Deg2Rad * m_LaserScanReader.m_AngularResolution,
                    time_increment = timeIncrement,
                    range_min = m_LaserScanReader.m_RangeMinimum,
                    range_max = m_LaserScanReader.m_RangeMaximum,
                    ranges = new float[numLines],
                    intensities = new float[numLines]
                };

            }
            else
            {
                message = new Messages.Sensor.LaserScan
                {
                    header = new Messages.Standard.Header { frame_id = FrameId },
                    angle_min = 0,
                    angle_max = 0,
                    angle_increment = 0,
                    time_increment = 0,
                    range_min = 0,
                    range_max = 0,
                    ranges = new float[0],      // length?
                    intensities = new float[0]
                };
            }
        }

        private void UpdateMessage()
        {
            message.header.Update();
            UpdateLaserScan();
            if(m_SimulationTime != null)
            {
                message.header.stamp.secs = m_SimulationTime.adjustedSeconds;
                message.header.stamp.nsecs = m_SimulationTime.adjustedNanoSeconds;
            }
            Publish(message);
        }

        private void UpdateLaserScan()
        {
            // Call LaserScanReader and update LaserScan states  
            m_LaserScanReader.UpdateScan(ref message.ranges, ref message.intensities);
        }
    }
}
