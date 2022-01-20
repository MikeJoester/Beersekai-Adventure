using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelAt)
                levelButtons[i].interactable = false;
        }
    }

    public void ChangeScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
