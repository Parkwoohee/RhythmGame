/*
If you use or adapt this software in your research please cite:

Afonso Gonçalves and Sergi Bermúdez i Badia. 2018.
KAVE: Building Kinect Based CAVE Automatic Virtual Environments, Methods for Surround-Screen Projection Management, Motion Parallax and Full-Body Interaction Support.
PACM on Human-Computer Interaction, Vol. 1, EICS, Article 10 (June 2018), 15 pages.
https://doi.org/10.1145/3229092

The author can be contacted at afonso.goncalves@m-iti.org
 */

using Windows.Kinect;
using UnityEngine;

namespace VRKave
{
    public class VRKinectBodySource : MonoBehaviour
    {
        public KinectSensor Sensor { get; private set; }
        private BodyFrameReader _reader;
        private Body[] _data = null;
        public Windows.Kinect.Vector4 Floor { get; private set; }

        public Body[] GetData()
        {
            return _data;
        }

        void Start()
        {
            Sensor = KinectSensor.GetDefault();
            if (Sensor != null)
            {
                _reader = Sensor.BodyFrameSource.OpenReader();
                if (!Sensor.IsOpen)
                    Sensor.Open();
            }
        }

        void Update()
        {
            if (Sensor == null)
                Sensor = KinectSensor.GetDefault();

            if (Sensor != null)
            {
                if (!Sensor.IsOpen)
                    Sensor.Open();

                if (Sensor.IsOpen)
                {
                    if (_reader == null)
                        _reader = Sensor.BodyFrameSource.OpenReader();
                    else
                    {
                        UpdateBodyData();
                    }
                }
            }
        }

        private void UpdateBodyData()
        {
            var frame = _reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_data == null)
                    _data = new Body[Sensor.BodyFrameSource.BodyCount];
                frame.GetAndRefreshBodyData(_data);
                Floor = frame.FloorClipPlane;
                frame.Dispose();
                //frame = null;
            }
        }

        void OnApplicationQuit()
        {
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            if (Sensor == null) return;
            if (Sensor.IsOpen)
                Sensor.Close();
            Sensor = null;
        }
    }
}
