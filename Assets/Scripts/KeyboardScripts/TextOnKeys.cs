// ABSTRACTION. In this, like in all parent classes of the project, I have organised code into methods as needed for clarity.
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Note: other classes from other scripts will inherit from this class, giving more sense to some of the things on it.
public class TextOnKeys : MonoBehaviour
{
    protected TMP_Text m_TextComponent;
    protected string notationBasedText;
    public bool alphabeticalNotation;
    protected int numberToDisplay;
    protected readonly List<string> alphabeticalNotes = new List<string>() { "A", "B", "C", "D", "E", "F", "G" };
    protected readonly List<string> syllabicNotes = new List<string>() { "La", "Si", "Do", "Re", "Mi", "Fa", "Sol" };
    protected readonly List<string> alphabeticalAccidentals = new List<string>() { "A#  ", "C#  ", "D#  ", "F#  ", "G#  "};
    protected readonly List<string> syllabicAccidentals = new List<string>() { "La# ", "Do# ", "Re# ", "Fa# ", "S.l# " };
    protected TMP_Text keyText;

    public void UpdateText(int notesSetForTextUpdate)
    {
        ChooseNotationTypeToDisplay();
        ChooseNumberToDisplay(notesSetForTextUpdate);
        WriteTextOnTheKey();
    }

    protected void ChooseNotationTypeToDisplay()
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
            Debug.Log("Main Manager not found. Was the scene loaded directly? Not a problem for notation type, we will default to syllabic.");
            SetToSyllabicNotation();
        }
    }

    protected int ChooseNumberToDisplay(int notesSetForTextUpdate)
    {
        if (alphabeticalNotation)
        {
            // Tags for these are on the canvases of the TMP text objects this script is attached to
            if (transform.parent.CompareTag("MajOctave 0"))
            {
                numberToDisplay = notesSetForTextUpdate;
            } else if (transform.parent.CompareTag("MajOctave 1"))
            {
                numberToDisplay = notesSetForTextUpdate + 1;
            } else if (transform.parent.CompareTag("MajOctave 2"))
            {
                numberToDisplay = notesSetForTextUpdate + 2;
            } else if (transform.parent.CompareTag("MajOctave 3"))
            {
                numberToDisplay = notesSetForTextUpdate + 3;
            }
        } else
        {
            // Tags for these are directly on the TMP text objects
            if (gameObject.CompareTag("MinOctave 0"))
            {
                numberToDisplay = notesSetForTextUpdate;
            } else if (gameObject.CompareTag("MinOctave 1"))
            {
                numberToDisplay = notesSetForTextUpdate + 1;
            } else if (gameObject.CompareTag("MinOctave 2"))
            {
                numberToDisplay = notesSetForTextUpdate + 2;
            } else if (gameObject.CompareTag("MinOctave 3"))
            {
                numberToDisplay = notesSetForTextUpdate + 3;
            }
        }
        return numberToDisplay;
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

    public void Awake()
    {
        keyText = GetComponent<TMP_Text>();
    }

    protected void WriteTextOnTheKey()
    {
        m_TextComponent = GetComponent<TMP_Text>();
        m_TextComponent.text = notationBasedText + numberToDisplay;
    }

    protected void OnEnable()
    {
        Awake();   
    }
}