using System;
using System.Collections;
using System.Collections.Generic;
using Script.Base;
using TMPro;
using UnityEngine;

public class TimeGlob : MonoBehaviour
{
    

    public TextMeshProUGUI scoreText;

    public float ttime;

    private void Start()
    {
        ttime = 60 * 8;
    }

    // Update is called once per frame
    void Update()
    {
        ttime -= Time.deltaTime;
        scoreText.text = "Time : " + ttime.ToString();

        if (ttime <= 0)
        {
            PlayTime.instance.EndGameLose();
        }
        
    }
}
