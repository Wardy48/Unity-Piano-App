using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.IO;
using UnityEngine.Animations;

public class TextOnKeys : MonoBehaviour
{
    protected TMP_Text m_TextComponent;
    protected string notationBasedText;
    public bool alphabeticalNotation;
    protected int selectedNotesSet;
    protected int numberToDisplay;
    protected readonly List<string> alphabeticalNotes = new List<string>() { "A", "B", "C", "D", "E", "F", "G" };
    protected readonly List<string> syllabicNotes = new List<string>() { "La", "Si", "Do", "Re", "Mi", "Fa", "Sol" };
    protected readonly List<string> alphabeticalAccidentals = new List<string>() { "A#  ", "C#  ", "D#  ", "F#  ", "G#  "};
    protected readonly List<string> syllabicAccidentals = new List<string>() { "La# ", "Do# ", "Re# ", "Fa# ", "S.l# " };
    protected TMP_Text keyText;

    // ABSTRACTION. In this, like in all parent classes of the project, I have organised code into methods as needed for clarity.
    public virtual void UpdateText()
    {
        ChooseNoteToDisplay();
        GrabSelectedNotesSetFromItalianKeysToPiano();
        ChooseNumberToDisplay();
        WriteTextOnTheKeyAndCheckForOutOfBounds();
    }

    protected int GrabSelectedNotesSetFromItalianKeysToPiano()
    {
        GameObject gameManager = GameObject.Find("Game Manager");

        if (gameManager != null)
        {
            ItalianKeysToPiano italianKeysToPiano = gameManager.GetComponent<ItalianKeysToPiano>();

            if (italianKeysToPiano != null)
            {
                selectedNotesSet = italianKeysToPiano.SelectedNotesSet;
            }
            else
            {
                Debug.Log("Game Manager does not have ItalianKeysToPiano script attached");
            }
        }
        else
        {
            Debug.Log("Game Manager not found");
        }
        return selectedNotesSet;
    }

    protected int ChooseNumberToDisplay()
    {
        if (alphabeticalNotation)
        {
            // Tags for these are on the canvases of the TMP text objects this script is attached to
            if (transform.parent.CompareTag("MajOctave 0"))
            {
                numberToDisplay = selectedNotesSet;
            } else if (transform.parent.CompareTag("MajOctave 1"))
            {
                numberToDisplay = selectedNotesSet + 1;
            } else if (transform.parent.CompareTag("MajOctave 2"))
            {
                numberToDisplay = selectedNotesSet + 2;
            } else if (transform.parent.CompareTag("MajOctave 3"))
            {
                numberToDisplay = selectedNotesSet + 3;
            }
        } else
        {
            // Tags for these are directly on the TMP text objects
            if (gameObject.CompareTag("MinOctave 0"))
            {
                numberToDisplay = selectedNotesSet;
            } else if (gameObject.CompareTag("MinOctave 1"))
            {
                numberToDisplay = selectedNotesSet + 1;
            } else if (gameObject.CompareTag("MinOctave 2"))
            {
                numberToDisplay = selectedNotesSet + 2;
            } else if (gameObject.CompareTag("MinOctave 3"))
            {
                numberToDisplay = selectedNotesSet + 3;
            }
        }
        return numberToDisplay;
    }

    protected void ChooseNoteToDisplay()
    {
        GameObject mainManager = GameObject.Find("MainManager");
        if (mainManager != null)
        {
            MainManager mainManagerScript = mainManager.GetComponent<MainManager>();
            if (mainManagerScript.syllabicNotation)
            {
                SetToSyllabicNotation();
            } else
            {
                SetToAlphabeticalNotation();
            }
        } else
        {
            Debug.Log("Main Manager not found. Was the scene loaded directly?");
            SetToSyllabicNotation();
        }
    }
    
    protected virtual void SetToAlphabeticalNotation()
    {
        alphabeticalNotation = true;
    }

    protected virtual void SetToSyllabicNotation()
    {
        keyText.fontSize = 3;
        alphabeticalNotation = false;   
    }

    public virtual void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectedNotesSet != 0)
            {
                selectedNotesSet--;
                UpdateText();
            }
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectedNotesSet < 7)
            {
                selectedNotesSet++;
                UpdateText();
            }
        }
    }

    public void Awake()
    {
        keyText = GetComponent<TMP_Text>();
        UpdateText();
    }

    protected void WriteTextOnTheKeyAndCheckForOutOfBounds()
    {
        m_TextComponent = GetComponent<TMP_Text>();
        m_TextComponent.text = notationBasedText + numberToDisplay;
        // CheckForOutOfBounds(); (Nah, no longer need this Method.)
    }

    protected void OnEnable()
    {
        Awake();   
    }
}