using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHandAndFoot : MonoBehaviour
{
    private GameObject rHand, lHand, rFoot, lFoot, rightGlove, leftGlove;
    // Start is called before the first frame update
    void Start()
    {
        rightGlove = GameObject.Find("RightGlove");
        leftGlove = GameObject.Find("LiftGlove");
        rHand = GameObject.Find("Controller (right)");
        lHand = GameObject.Find("Controller (left)");
    }

    // Update is called once per frame
    void Update()
    {
        rightGlove.transform.position = rHand.transform.position;
        rightGlove.transform.rotation = rHand.transform.rotation;
        leftGlove.transform.position = lHand.transform.position;
        leftGlove.transform.rotation = lHand.transform.rotation;
    }
}
