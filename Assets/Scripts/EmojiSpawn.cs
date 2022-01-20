using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpawn : MonoBehaviour
{
    public GameObject[] SadEmotes;
    public GameObject[] HappyEmotes;
    public static bool spawned = false;

    void SpawnEmoji()
    {
        GameObject Emote = HappyEmotes[Random.Range(0, HappyEmotes.Length)];
        GameObject obj = Instantiate(Emote) as GameObject;
        obj.transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, -2.08f);
    }

    void SpawnSadEmoji()
    {
        GameObject Emote = SadEmotes[Random.Range(0, SadEmotes.Length)];
        GameObject obj2 = Instantiate(Emote) as GameObject;
        obj2.transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, -2.08f);
    }

    void Update()
    {
        if (!spawned && BeerBottles.Streak != 0 && BeerBottles.Streak % 5 == 0)
            {
                spawned = true;
                SpawnEmoji();
            }

        if (HealthManagement.life == 2 && !spawned && EmoteController.currentLife == 3)
        {
            spawned = true;
            SpawnSadEmoji();
        }

        if (HealthManagement.life == 1 && !spawned && EmoteController.currentLife == 2)
        {
            spawned = true;
            SpawnSadEmoji();
        }
    }
}
