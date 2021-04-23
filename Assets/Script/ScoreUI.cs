using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static Text scoreText;
    public static int Score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        scoreText.color = Color.green;
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Scroe : " + Score;
    }

    public static void GameOver()
    {
        scoreText.color = Color.red;
        scoreText.text = " Game Over!!";
    }
}
