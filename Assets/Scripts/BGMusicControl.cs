using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMusicControl : MonoBehaviour
{
    private static BGMusicControl backgroundMusic;
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    void Start()
    {
        UpdateButton();
    }

    public void OnButtonPress()
    {
        if (!muted)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (!muted)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    void Awake()
    {
        if (backgroundMusic == null)
            {
                backgroundMusic = this;
                DontDestroyOnLoad(backgroundMusic);
            }
            else
            {
                Destroy(gameObject);
            }
    }
}
