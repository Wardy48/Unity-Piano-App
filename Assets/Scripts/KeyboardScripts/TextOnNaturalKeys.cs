using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE. The following class, like TextOnAccidentalKeys, performs all the same actions from TextOnKeys.
public class TextOnNaturalKeys : TextOnKeys
{
    // POLYMORPHISM: The two method overrides below add new code to the original method in TextOnKeys, as well as running the base one.
    protected override void SetToAlphabeticalNotation()
    {
        // I know the labels to recognise these are in syllabic, but this is to set to ALPHABETICAL notation!
        if (transform.parent.parent.CompareTag("La"))
        {
            notationBasedText = alphabeticalNotes[0];
        } else if (transform.parent.parent.CompareTag("Si"))
        {
            notationBasedText = alphabeticalNotes[1];
        } else if (transform.parent.parent.CompareTag("Do"))
        {
            notationBasedText = alphabeticalNotes[2];
        } else if (transform.parent.parent.CompareTag("Re"))
        {
            notationBasedText = alphabeticalNotes[3];
        } else if (transform.parent.parent.CompareTag("Mi"))
        {
            notationBasedText = alphabeticalNotes[4];
        } else if (transform.parent.parent.CompareTag("Fa"))
        {
            notationBasedText = alphabeticalNotes[5];
        } else if (transform.parent.parent.CompareTag("Sol"))
        {
            notationBasedText = alphabeticalNotes[6];
        }
        base.SetToAlphabeticalNotation();
    }

    protected override void SetToSyllabicNotation()
    {
        // I know the labels to recognise these are in syllabic, but this is to set to ALPHABETICAL notation!
        if (transform.parent.parent.CompareTag("La"))
        {
            notationBasedText = syllabicNotes[0];
        } else if (transform.parent.parent.CompareTag("Si"))
        {
            notationBasedText = syllabicNotes[1];
        } else if (transform.parent.parent.CompareTag("Do"))
        {
            notationBasedText = syllabicNotes[2];
        } else if (transform.parent.parent.CompareTag("Re"))
        {
            notationBasedText = syllabicNotes[3];
        } else if (transform.parent.parent.CompareTag("Mi"))
        {
            notationBasedText = syllabicNotes[4];
        } else if (transform.parent.parent.CompareTag("Fa"))
        {
            notationBasedText = syllabicNotes[5];
        } else if (transform.parent.parent.CompareTag("Sol"))
        {
            notationBasedText = syllabicNotes[6];
        }
        base.SetToSyllabicNotation();
    }
}
