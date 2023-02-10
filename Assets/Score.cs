using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public int a;

    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        a = score;


        scoreText.text = "Score : " + score.ToString();
    }
}