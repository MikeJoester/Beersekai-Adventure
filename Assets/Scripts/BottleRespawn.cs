using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottleRespawn : MonoBehaviour
{
    public GameObject[] BeerType;
    public float respawnTime;
    private Vector2 screenBounds;
    public int xbounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Bottles());
    }

    private void spawnBottle()
    {
        GameObject beerb = BeerType[Random.Range(0, BeerType.Length)];
        GameObject lr = Instantiate(beerb) as GameObject;
        lr.transform.position = new Vector2(Random.Range(-screenBounds.x, xbounds) , screenBounds.y);
    }

    IEnumerator Bottles()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnBottle();
        }
    }
}
