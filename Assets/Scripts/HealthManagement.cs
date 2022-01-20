using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    public GameObject[] hearts;
    public static int life = 3;

    public void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public static void TakeDmg()
    {
        SoundManager.GetHurt();
        life -= 1;
        if (life == 0)
        {
            PlayfabManager.SendLeaderBoard(BeerBottles.TotalScore);
        }
    }
}
