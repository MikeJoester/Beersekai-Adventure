using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeVal = 91;
    public Text timeText;
    bool updated = false;

    void Update()
    {
        if (timeVal > 0)
        {
            timeVal -= Time.deltaTime;
        }
        else
        {
            if (!updated)
            {
                PlayfabManager.SendLeaderBoard(BeerBottles.TotalScore);
                updated = true;
            }
        }
        DisplayTimer(timeVal);
    }

    void DisplayTimer(float Time2Display)
    {
        if (Time2Display < 0)
        {
            Time2Display = 0;
        }
        
        float mins = Mathf.FloorToInt(Time2Display / 60);

        float secs = Mathf.FloorToInt(Time2Display % 60);

        timeText.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}
