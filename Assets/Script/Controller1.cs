using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller1 : MonoBehaviour {
    public GameObject controller;
    public Vector3 previous;
    public float controllerSpeed;

    void Start () {

    }
	
	void Update () {
        controllerSpeed = ((controller.transform.position - previous).magnitude);
        previous = controller.transform.position;
    }
    
    public bool ControllerVelocity()
    {
        if (controllerSpeed > 0.005f)
        {
            return true;
        }
        else
            return false;
    }
}
