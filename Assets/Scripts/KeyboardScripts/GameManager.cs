using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject naturals;
    [SerializeField] GameObject accidentals;
    [SerializeField] GameObject leftmostKeyText;
    [SerializeField] GameObject keyBindingsCanvas;
    [SerializeField] GameObject defaultKeyboardCanvas;
    [SerializeField] GameObject separatorLine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ToggleTextOnKeys(naturals.transform);
            ToggleTextOnKeys(accidentals.transform);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleSeparatorLine();
        }
    }

    void ToggleTextOnKeys(Transform parent)
    {
        bool alphabeticalNotation = SeeWhichNotationIsEnabled();
        for (int i = 0; i < parent.childCount; i++)
        {
            // Both the "NATURALS" parent and "ACCIDENTALS" parent have mostly keys as their children, but also four empty objects as children (two for "NATURALS" and two for "ACCIDENTALS") that are the parents of other keys in the same way, but these parents' tag names contain "keys that can despawn". Hence, the following "if" statement toggles/untoggles text from children that do not contain "keys that can despawn" in the tag name, and reiterates itself for those that do, treating the latter children as parents.
            if(!parent.GetChild(i).name.Contains("Despawn-on-set-5"))
            {
                Transform pianoKey = parent.GetChild(i);
                if(alphabeticalNotation)
                {ToggleIfNotLabelledLa(pianoKey);}
                else{ToggleIfNotLabelledDo(pianoKey);}
            } else
            {
                ToggleTextOnKeys(parent.GetChild(i).transform);
            }
        }
    }

    bool SeeWhichNotationIsEnabled()
    {
        // We will do this by checking the text notation on the leftmost key of the keyboard ("A0 - La0 TMP Text") see the serialized field further up called "leftmostKeyText" that allows us to assign that object via the Unity editor.
        TextOnNaturalKeys textOnNaturalKeys = leftmostKeyText.GetComponent<TextOnNaturalKeys>();
        bool alphabeticalNotation = textOnNaturalKeys.alphabeticalNotation; 
        return alphabeticalNotation;
    }

    void ToggleIfNotLabelledLa(Transform pianoKey)
    {
        Transform canvasOfTheKey = pianoKey.GetChild(0);
       if ((!pianoKey.CompareTag("La")) && canvasOfTheKey.gameObject.activeSelf)
       {
            canvasOfTheKey.gameObject.SetActive(false);
       } else if (!pianoKey.CompareTag("La"))
       {
            canvasOfTheKey.gameObject.SetActive(true);
       }
    }

    void ToggleIfNotLabelledDo(Transform pianoKey)
    {
        Transform canvasOfTheKey = pianoKey.GetChild(0);
       if ((!pianoKey.CompareTag("Do")) && canvasOfTheKey.gameObject.activeSelf)
       {
            canvasOfTheKey.gameObject.SetActive(false);
       } else if (!pianoKey.CompareTag("Do"))
       {
            canvasOfTheKey.gameObject.SetActive(true);
       }
    }

    public void LoadMenuScreenScene()
    {
        SceneManager.LoadScene("Menu Screen");
    }

    public void LoadKeyBindingsCanvas()
    {
        keyBindingsCanvas.SetActive(true);
        defaultKeyboardCanvas.SetActive(false);
    }

    public void LoadDefaultKeyboardCanvas()
    {
        defaultKeyboardCanvas.SetActive(true);
        keyBindingsCanvas.SetActive(false);
    }

    public void ToggleSeparatorLine()
    {
        if (!separatorLine.activeSelf)
        {
            separatorLine.SetActive(value: true);
        } else
        {
            separatorLine.SetActive(value: false);
        }
    }

    internal static void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }
}