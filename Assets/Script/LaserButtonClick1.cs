using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR;
using UnityEngine.SceneManagement;


public class LaserButtonClick1 : MonoBehaviour
{
    private SteamVR_Action_Boolean act;

    // Start is called before the first frame update
    void Start()
    {
        act = SteamVR_Input.GetBooleanAction("GrabPinch");
        //menu = GameObject.Find("menu");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Cube")
        {
            this.GetComponent<MeshRenderer>().material.color = Color.yellow;
            if (act.GetState(SteamVR_Input_Sources.RightHand))
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Cube")
        {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
