using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject WinningUI;
    public GameObject GameOverUI;
    public GameObject In4UI;
    public GameObject LoginUI;
    public Text scoreText;
    public Text highScoreDisplay;
    public Text BottleStreak;
    public Text PlayerName;
    bool isOpen;
    private bool in4off = false;
    public static bool IsPaused = false;
    //public int PassedScore;

    void Start()
    {
        string DpName = PlayerPrefs.GetString("name");
        PlayerName.text = "Greetings, " + DpName + "!";
    }

    void Update()
    {
        scoreText.text = BeerBottles.TotalScore.ToString();

        if (BeerBottles.Streak > 4)
            BottleStreak.text = BeerBottles.Streak.ToString() + "X"; 
        else BottleStreak.text = "";

        if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P)) && HealthManagement.life != 0)
        {
            if (IsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }

        string sc = BeerBottles.TotalScore.ToString();
        string currentScene = SceneManager.GetActiveScene().name;
        if ((!(Timer.timeVal > 0)) & currentScene != "TimeAttack" & currentScene != "Multiplayer")
        {
            highScoreDisplay.text = "Your Score: " + BeerBottles.TotalScore.ToString();
            WinningUI.SetActive(true);
            IsPaused = true;
            Time.timeScale = 0f;
        }

        if ((HealthManagement.life == 0) || (!(Timer.timeVal > 0)) & (currentScene == "TimeAttack" || currentScene == "Multiplayer"))
        {
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }

    public void GameModes()
    {
        if (SceneManager.GetActiveScene().name == "TimeAttack" || SceneManager.GetActiveScene().name == "Multiplayer") 
            SceneManager.LoadScene("GameMode");
        else SceneManager.LoadScene("StageSelect");
        Resume();
        HealthManagement.life = 3;
        BeerBottles.TotalScore = 0;
        BeerBottles.Streak = 0;
        Timer.timeVal = 91;
        EmojiSpawn.spawned = false;
        EmoteController.currentLife = 3;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
        HealthManagement.life = 3;
        BeerBottles.TotalScore = 0;
        BeerBottles.Streak = 0;
        Timer.timeVal = 91;
        EmojiSpawn.spawned = false;
        EmoteController.currentLife = 3;
    }

    public void OpenLoginUI()
    {
        if (LoginUI != null)
        {
            Animator animator = LoginUI.GetComponent<Animator>();
            if (animator != null)
            {
                isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }

    public void OpenIn4UI()
    {
        if (!in4off)
        {
            in4off = true;
        }
        else
        {
            in4off =  false;
        }
        In4UIUpdate();
    }

    private void In4UIUpdate()
    {
        if (!in4off)
        {
            In4UI.SetActive(false);
        }
        else
        {
            In4UI.SetActive(true);
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Menu()
    {
        SceneManaging.Return2Menu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
