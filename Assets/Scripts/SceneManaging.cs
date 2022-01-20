using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("NewPlayerInput");
    }

    public void ProfileManager()
    {
        SceneManager.LoadScene("SaveLoadManager");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public static void Return2Menu()
    {
        DialogueControl.textSpeed = 0.08f;
        HealthManagement.life = 3;
        BeerBottles.TotalScore = 0;
        BeerBottles.Streak = 0;
        Timer.timeVal = 91;
        EmojiSpawn.spawned = false;
        EmoteController.currentLife = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }
    
    public void TimeAttack()
    {
        SceneManager.LoadScene("TimeAttack");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Next()
    {
        DialogueControl.textSpeed = 0.08f;
        HealthManagement.life = 3;
        BeerBottles.TotalScore = 0;
        BeerBottles.Streak = 0;
        Timer.timeVal = 91;
        EmojiSpawn.spawned = false;
        EmoteController.currentLife = 3;
        Time.timeScale = 1f;
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextScene > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", NextScene);
        }
        SceneManager.LoadScene(NextScene);
    }

    public void Return2Login()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
