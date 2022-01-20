using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteController : MonoBehaviour
{
    float fadeSpeed = 0.5f;
    private Rigidbody2D Emote;
    public static int currentLife = 3;

    void Start()
    {
        Emote = this.GetComponent<Rigidbody2D>();
        Emote.velocity = new Vector2(0, 1.2f);
    }

    void Update()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;

        if (objectColor.a <= 0)
        {
            EmojiSpawn.spawned = false;
            Destroy(this.gameObject);
            currentLife -= 1;
        }
    }
}
