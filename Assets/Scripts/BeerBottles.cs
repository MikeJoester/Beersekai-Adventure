using UnityEngine;
using UnityEngine.SceneManagement;

public class BeerBottles : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public static int TotalScore = 0;
    public static int Streak = 0;
    float speed;
    public static int point;
    private Rigidbody2D bottle;
    private Vector2 screenBounds;

    void Start()
    {
        bottle = this.GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        if (500 < TotalScore & TotalScore < 1000)
        {
            minSpeed = minSpeed * 1.2f;
            maxSpeed = maxSpeed * 1.2f;
        }
        if (1000 < TotalScore & TotalScore < 2000)
        {
            minSpeed = minSpeed * 1.5f;
            maxSpeed = maxSpeed * 1.5f;
        }
        if (2000 < TotalScore)
        {
            minSpeed = minSpeed * 1.7f;
            maxSpeed = maxSpeed * 1.7f;
        }
        bottle.velocity = new Vector2(0, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TimeAttack")
        {
            if (transform.position.y < (screenBounds.y + 1) * -1)
            {
                Streak = 0;
                Destroy(this.gameObject);
            }
        }
        else 
        {
            if (transform.position.y < (screenBounds.y - 1) * -1)
            {
                if (bottle.name.ToString() == "AmogusLord(Clone)")
                {
                    SoundManager.AmogusDead();
                    Destroy(this.gameObject);
                }
                else
                {
                    HealthManagement.TakeDmg();
                    Streak = 0;
                    Destroy(this.gameObject);
                }
            }   
        }
    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Player")
        {
            switch (bottle.name.ToString())
            {
                case "Larue(Clone)":
                    point = 10;
                    SoundManager.playsound();
                    break;
                case "Corona(Clone)":
                    point = 15;
                    SoundManager.playsound();
                    break;
                case "Ken(Clone)":
                    point = 20;
                    SoundManager.playsound();
                    break;
                case "AmogusLord(Clone)":
                    point = -100;
                    Streak = 0;
                    SoundManager.AmogusSound();
                    break;
                case "Cheems(Clone)":
                    point = 30;
                    Timer.timeVal += 5;
                    SoundManager.CheemsSound();
                    break;
            }
            TotalScore = TotalScore + point + Streak ;
            Streak += 1;
            Destroy(this.gameObject);
        }
    }
}
