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
using Vector4 = UnityEngine.Vector4;

namespace VRKave
{
    public class JointPositionControl : MonoBehaviour
    {
        private GameObject _kinectBodySource;
        private VRKinectBodySource _bodySourceManager;
        private VRWorldManager _vrWorldManager;
        private int _bodyIndex = 0;
        private Matrix4x4 _fromKinecttoUnity = Matrix4x4.zero;

        private readonly int[,] _bonechains = new int[5, 4]
            {{0, 1, 20, 2}, {0, 12, 13, 14}, {0, 16, 17, 18}, {20, 4, 5, 6}, {20, 8, 9, 10}};

        public GameObject[] AvatarBones;

        void Start()
        {
            _kinectBodySource = gameObject.transform.parent.gameObject;

            if (_kinectBodySource != null)
                _bodySourceManager = _kinectBodySource.GetComponent<VRKinectBodySource>();

            _vrWorldManager = gameObject.GetComponentInParent<VRWorldManager>();

            _fromKinecttoUnity[0, 0] = -1;
            _fromKinecttoUnity[1, 1] = 1;
            _fromKinecttoUnity[2, 2] = 1;
        }

        void FixedUpdate()
        {
            var kinectBody = GetKinectBody();
            if (kinectBody == null)
                return;
            MoveBones(kinectBody);
            ResizeBones();
        }

        private void ResizeBones()
        {
            float boneWidth = .05f * _vrWorldManager.KaveScale;
            for (int chain = 0; chain < _bonechains.GetLength(0); chain++)
            {
                for (int bone = 1; bone < _bonechains.GetLength(1); bone++)
                {
                    var boneLenght = (AvatarBones[_bonechains[chain, bone]].transform.position -
                                      AvatarBones[_bonechains[chain, bone - 1]].transform.position).magnitude;

                    AvatarBones[_bonechains[chain, bone]].transform.GetChild(0).transform.position =
                        AvatarBones[_bonechains[chain, bone]].transform.position -
                        (AvatarBones[_bonechains[chain, bone]].transform.position -
                         AvatarBones[_bonechains[chain, bone - 1]].transform.position) / 2;

                    AvatarBones[_bonechains[chain, bone]].transform.GetChild(0).transform.localScale =
                        new Vector3(boneWidth, boneLenght, boneWidth);
                }
            }


        }

        private void MoveBones(Body kinectBody)
        {
            int kinectJoint = -1;
            foreach (var bone in AvatarBones)
            {
                if (bone == null)
                    break;
                kinectJoint++;
                bone.transform.localPosition = Vector3.zero;

                if (kinectBody.IsTracked)
                {
                    var kinectPosition = GetVector3FromJoint(kinectBody.Joints[(JointType) kinectJoint]) *
                                         _vrWorldManager.KaveScale;
                    Vector4 tempKinectPosition = kinectPosition;
                    var tempUnityPosition = _fromKinecttoUnity * tempKinectPosition;
                    Vector3 unityPosition = tempUnityPosition;
                    bone.transform.position = _kinectBodySource.transform.position +
                                              _kinectBodySource.transform.rotation * unityPosition;
                }
            }
        }

        private Body GetKinectBody()
        {
            if (_kinectBodySource == null)
                return null;

            if (_bodySourceManager == null)
                _bodySourceManager = _kinectBodySource.GetComponent<VRKinectBodySource>();

            if (_bodySourceManager == null)
                return null;

            Body[] kinectBodies = _bodySourceManager.GetData();
            if (kinectBodies == null)
                return null;

            var kinectBody = kinectBodies[_bodyIndex];
            return kinectBody;
        }

        private static Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint)
        {
            return new Vector3(joint.Position.X, joint.Position.Y, joint.Position.Z);
        }

        public void SetBodyIndex(int index)
        {
            _bodyIndex = index;
        }
    }
}
