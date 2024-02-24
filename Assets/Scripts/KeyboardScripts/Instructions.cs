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
        +"Right shift\n\n"
        +"<b>Toggle separation line for computer keyboard reference:</b>\n"
        +"Tab\n\n"
        +"<b>Play notes:</b>\n"
        +"Q through to the rightmost key of Q’s row"
        +"(what computer key this is depends on your keyboard variant) play the naturals"
        +"up to the second E/Mi."
        +"The keys above this row play the corresponding accidentals.\n"
        +"Left shift through to the key next to right shift"
        +"(again, what computer key this is will depend)"
        +"play the naturals from the second F/Fa to the far right’s B/Si."
        +"The keys above this row are the corresponding accidentals.\n"
        +"I understand this may not be the best fit for your keyboard. "
        +"Unfortunately, Unity only takes key codes for US keyboards.";
    }
}