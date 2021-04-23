using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public static int LifeBarControll;
    // Start is called before the first frame update
    void Start()
    {
        LifeBarControll = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeBarControll >= 1)
        {
            this.transform.localScale = new Vector3(1, LifeBarControll, 1);
        }
        else
        {
            ScoreUI.GameOver();
        }
    }
}
