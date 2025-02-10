public class TextOnAccidentalKeys : TextOnKeys
{
   // When you read transform.parent.parent, it's because this script will be attached to TMP Text objects. Their parent is their canvas, and their canvas's parent is the key that canvas is of, a key which will have the tag being sought here.
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
        base.SetToAlphabeticalNotation();
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
        // NOT using the base of this method, I instead did something special (and felicitous) with the font size that is not done in the text on natural keys (at the time of writing this).
    }
}