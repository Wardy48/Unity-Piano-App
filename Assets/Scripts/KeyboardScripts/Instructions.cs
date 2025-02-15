using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions : MonoBehaviour
{
    [SerializeField] TMP_Text instructions;
    // Start is called before the first frame update
    void Start()
    {
        instructions.text = "<b>Toggle most of the texts on the keys:</b>\n"
        +"<i>Right Shift</i>\n\n"
        +"<b>Toggle separation line for computer keyboard reference:</b>\n"
        +"<i>Tab</i>\n\n"
        +"<b>Change octaves:</b>\n"
        +"<i>Left Arrow</i> and <i>Right Arrow</i>\n\n"
        +"<b>Play notes:</b>\n"
        +"The naturals from the first A/La (A0/La0 on the first octaves set) up to the second E/Mi (E1/Mi2 on the first octaves set)\n"
        +"are played by <i>Q</i> through to two keys to the right of <i>P</i> (what computer key this is depends on your keyboard variant).\n"
        +"The computer keys above this row play the corresponding accidentals.\n"
        +"The naturals from the second F/Fa (F1/Fa2 on the first octaves set) to the far right’s B/Si (B3/Si3 on the first octaves set)\n"
        +"are played by <i>Left Shift</i> through to three keys to the right of <i>M</i> (again, what computer key this is depends on your keyboard variant).\n"
        +"Again, the computer keys above this row play the corresponding accidentals.\n\n"
        +"<b>Because of indexing being inherent to ANSI keyboards, the piano key right of <i>Left Shift</i>'s piano key is played by <i>Z</i>.\n"
        +"The key in between these two on ISO keyboards is not usable, but you can still play all piano keys.</b>";
    }
}