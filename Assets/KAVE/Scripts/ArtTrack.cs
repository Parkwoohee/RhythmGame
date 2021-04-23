/*
If you use or adapt this software in your research please cite:

Afonso Gonçalves and Sergi Bermúdez i Badia. 2018.
KAVE: Building Kinect Based CAVE Automatic Virtual Environments, Methods for Surround-Screen Projection Management, Motion Parallax and Full-Body Interaction Support.
PACM on Human-Computer Interaction, Vol. 1, EICS, Article 10 (June 2018), 15 pages.
https://doi.org/10.1145/3229092

The author can be contacted at afonso.goncalves@m-iti.org
 */


using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace VRKave
{
    public class ArtTrack : MonoBehaviour
    {
        public GameObject[] Joint = new GameObject[1];
        private UDPReceiver _receiver;
        private Vector3 _headPosition = Vector3.zero;
        public int BodyID = 0;

        // Use this for initialization
        void Start()
        {
            _receiver = new UDPReceiver(this);
            if (_receiver != null)
                _receiver.StartReceiving();
        }

        // Update is called once per frame
        void Update()
        {
            //Todo: set the head orientation according to arttrack constelation
            Joint[0].transform.localPosition = _headPosition;
            //Joint[0].transform.localRotation = 
        }

        void OnDestroy()
        {
            _receiver.StopReceiving();
        }

        private class UDPReceiver
        {
            private readonly ArtTrack _artTrack;

            // receiving Thread
            private Thread _receiveThread;

            // udpclient object
            private UdpClient _client;
            private int _port; // define > init

            private readonly char[] _lineDelimiterChars = { '\n' };
            private const string Pattern6DOFHeader = @"^6d\s(\d+)";
            private const string Pattern6DOFBody = @"\[(\d+)\s-?\d+\.\d+\]\[(-?\d+\.\d+)\s(-?\d+\.\d+)\s(-?\d+\.\d+)\s(-?\d+\.\d+)\s(-?\d+\.\d+)\s(-?\d+\.\d+)\]\[-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\s-?\d+\.\d+\]";

            public UDPReceiver(ArtTrack artTrack)
            {
                _artTrack = artTrack;
            }

            // init
            public void StartReceiving()
            {
                // define port
                _port = 5000;
                _client = new UdpClient(_port);
                _receiveThread = new Thread(ReceiveData) { IsBackground = true };
                _receiveThread.Start();
            }

            // receive thread
            private void ReceiveData()
            {
                while (true)
                {
                    //Thread.Sleep(1000);
                    try
                    {
                        IPEndPoint senderIP = new IPEndPoint(IPAddress.Loopback, 0);
                        byte[] data = _client.Receive(ref senderIP);

                        string text = Encoding.UTF8.GetString(data);
                        //print(">> " + text);

                        var lines = text.Split(_lineDelimiterChars);

                        foreach (string line in lines)
                        {
                            var matches6DOFHeader = Regex.Matches(line, Pattern6DOFHeader);

                            if (matches6DOFHeader.Count != 1) continue;
                            if (matches6DOFHeader[0].Groups.Count != 2) continue;

                            var matches6DOFBodies = Regex.Matches(line, Pattern6DOFBody);
                            int numOfBodies = Int32.Parse(matches6DOFHeader[0].Groups[1].Value);

                            if (numOfBodies <= 0) continue;
                            foreach (Match body in matches6DOFBodies)
                            {
                                if (int.Parse(body.Groups[1].Value) != _artTrack.BodyID-1) continue;
                                _artTrack._headPosition = new Vector3(
                                    -float.Parse(body.Groups[2].Value) / 1000f,
                                    float.Parse(body.Groups[4].Value) / 1000f,
                                    -float.Parse(body.Groups[3].Value) / 1000f);
                                break;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        print(err.ToString());
                    }
                }
            }

            public void StopReceiving()
            {
                if (_receiveThread != null)
                {
                    _receiveThread.Abort();
                    _client.Close();
                }
                _receiveThread = null;
            }
        }
    }
}
