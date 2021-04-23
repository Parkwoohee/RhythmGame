/*
If you use or adapt this software in your research please cite:

Afonso Gonçalves and Sergi Bermúdez i Badia. 2018.
KAVE: Building Kinect Based CAVE Automatic Virtual Environments, Methods for Surround-Screen Projection Management, Motion Parallax and Full-Body Interaction Support.
PACM on Human-Computer Interaction, Vol. 1, EICS, Article 10 (June 2018), 15 pages.
https://doi.org/10.1145/3229092

The author can be contacted at afonso.goncalves@m-iti.org
 */

using UnityEngine;
using Windows.Kinect;

namespace VRKave
{
    public class BodySourceViewer : MonoBehaviour
    {
        public GameObject _avatar;

        private VRKinectBodySource _bodySource;
        private KinectSensor _sensor;
        private GameObject[] _avatars;
        private bool[] _activeAvatars;

        void Start()
        {
            _bodySource = gameObject.GetComponent<VRKinectBodySource>();
        }

        void Update()
        {

            if (_sensor == null)
            {
                _sensor = _bodySource.Sensor;
                if (_sensor == null) return;

                _avatars = new GameObject[_sensor.BodyFrameSource.BodyCount];
                _activeAvatars = new bool[_sensor.BodyFrameSource.BodyCount];
            }

            Body[] kinectBodies = _bodySource.GetData();
            if (kinectBodies == null)
            {
                //Destroy all avatars that exist
                for (int bodyIndex = 0; bodyIndex < _sensor.BodyFrameSource.BodyCount; bodyIndex++)
                {
                    if (_avatars[bodyIndex] == null) continue;

                    Destroy(_avatars[bodyIndex]);
                    _avatars[bodyIndex] = null;
                    _activeAvatars[bodyIndex] = false;
                }
                return;
            }

            for (int bodyIndex = 0; bodyIndex < _sensor.BodyFrameSource.BodyCount; bodyIndex++)
            {
                if (kinectBodies[bodyIndex] != null)
                {
                    if (!_activeAvatars[bodyIndex] && kinectBodies[bodyIndex].IsTracked)
                    {
                        _avatars[bodyIndex] = Instantiate(_avatar);
                        _avatars[bodyIndex].transform.SetParent(gameObject.transform);
                        _avatars[bodyIndex].GetComponent<JointPositionControl>().SetBodyIndex(bodyIndex);
                        _avatars[bodyIndex].GetComponent<JointOrientationControl>().SetBodyIndex(bodyIndex);
                        _activeAvatars[bodyIndex] = true;
                    }
                    else if (_activeAvatars[bodyIndex] && !kinectBodies[bodyIndex].IsTracked)
                    {
                        //Destroy the avatar if it exists but the corresponding body is not being tracked
                        Destroy(_avatars[bodyIndex]);
                        _avatars[bodyIndex] = null;
                        _activeAvatars[bodyIndex] = false;
                    }
                }
                else
                {
                    //Destroy the avatar if the body is null
                    Destroy(_avatars[bodyIndex]);
                    _avatars[bodyIndex] = null;
                    _activeAvatars[bodyIndex] = false;
                }
            }
        }
    }
}
