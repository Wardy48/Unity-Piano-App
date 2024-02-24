using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.IO;
using UnityEngine.Animations;

public class TextOnAccidentalKeys : TextOnKeys
{
    protected override void SetToAlphabeticalNotation()
    {
        if (transform.parent.parent.CompareTag("La#"))
        {
            notationBasedText = alphabeticalAccidentals[0];
        } else if (transform.parent.parent.CompareTag("Do#"))
        {
            notationBasedText = alphabeticalAccidentals[1];
        } else if (transform.parent.parent.CompareTag("Re#"))
        {
            notationBasedText = alphabeticalAccidentals[2];
        } else if (transform.parent.parent.CompareTag("Fa#"))
        {
            notationBasedText = alphabeticalAccidentals[3];
        } else if (transform.parent.parent.CompareTag("Sol#"))
        {
            notationBasedText = alphabeticalAccidentals[4];
        }
        alphabeticalNotation = true;
    }

    protected override void SetToSyllabicNotation()
    {
        keyText.fontSize = 3;
        if (transform.parent.parent.CompareTag("La#"))
        {
            keyText.fontSize = 4;
            notationBasedText = syllabicAccidentals[0];
        } else if (transform.parent.parent.CompareTag("Do#"))
        {
            keyText.fontSize = 4;
            notationBasedText = syllabicAccidentals[1];
        } else if (transform.parent.parent.CompareTag("Re#"))
        {
            keyText.fontSize = 4;
            notationBasedText = syllabicAccidentals[2];
        } else if (transform.parent.parent.CompareTag("Fa#"))
        {
            keyText.fontSize = 4;
            notationBasedText = syllabicAccidentals[3];
        } else if (transform.parent.parent.CompareTag("Sol#"))
        {
            keyText.fontSize = 4;
            notationBasedText = syllabicAccidentals[4];
        }
        alphabeticalNotation = false;
    }
}