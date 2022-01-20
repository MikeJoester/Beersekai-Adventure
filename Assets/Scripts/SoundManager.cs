using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static AudioClip[] AmberSound, EulaSound, RaidenSound, Amogus, Cheems;
    public static AudioClip[] GameSounds;

    static AudioSource AudioSrc;

    void Start()
    {
        GameSounds = Resources.LoadAll<AudioClip> ("Sounds/GameSounds");
        AmberSound = Resources.LoadAll<AudioClip> ("Sounds/Voice/Amber");
        RaidenSound = Resources.LoadAll<AudioClip> ("Sounds/Voice/Raiden");
        EulaSound = Resources.LoadAll<AudioClip> ("Sounds/Voice/Eula");
        Amogus = Resources.LoadAll<AudioClip> ("Sounds/Voice/Amogus");
        Cheems = Resources.LoadAll<AudioClip> ("Sounds/Voice/Cheems");
        AudioSrc = GetComponent<AudioSource>();
    }

    public static void playsound()
    {
        AudioSrc.PlayOneShot(GameSounds[2]);
    }

    public static void GetHurt()
    {
        AudioSrc.PlayOneShot(GameSounds[0]);
    }

    public static void AmogusSound()
    {
        AudioSrc.PlayOneShot(Amogus[0]);
    }

    public static void AmogusDead()
    {
        AudioSrc.PlayOneShot(Amogus[1]);
    }

    public static void CheemsSound()
    {
        AudioSrc.PlayOneShot(Cheems[0]);
    }
}
