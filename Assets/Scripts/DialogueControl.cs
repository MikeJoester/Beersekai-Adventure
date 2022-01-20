using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [Header("Dialogue")]
    public TextMeshProUGUI textBox;
    public string[] lines;
    public static float textSpeed = 0.08f;
    public static int CharPhase = 0;

    [Header("Character")]
    public Image Character;
    public Sprite[] spriteArray;
    public int AppearIndex;
    
    private int index;

    void Start()
    {
        Character.enabled = false;
        textBox.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textBox.text == lines[index])
            {
                if (CharPhase == AppearIndex)
                Character.enabled = true;

                NextLine();

                Character.sprite = spriteArray[CharPhase - AppearIndex];
            }
            else
            {
                StopAllCoroutines();
                textBox.text = lines[index];
                CharPhase += 1;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Character.enabled = false;
            textBox.text = string.Empty;
            CharPhase = 0;
            index = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
