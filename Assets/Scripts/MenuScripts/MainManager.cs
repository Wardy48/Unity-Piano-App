using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainManager : MonoBehaviour
{
    internal bool syllabicNotation;
    // TODO: make sure this selectedColour gets stored in JSON, for data persistence across sections.
    internal string selectedColour { get; private set; }
    internal static MainManager Instance;

    void Awake()
    {
        EnsureExactlyOneInstanceOfThisObjectExistsAndPersistsAcrossScenes();
    }

    void EnsureExactlyOneInstanceOfThisObjectExistsAndPersistsAcrossScenes()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else 
        {
            Destroy(gameObject);
            return;
        }
    }

    // TODO: yet to fully implement data persistance across scenes with the colour (the buttons don't stay highlighted when you go back to the menu), do it!
    // TODO: must also implement a default colour for the first time the player enters the game.
    // TODO: also ensure the text on the keys remains readable despite the highlighting, which can involve letting it temporarily change.

    public void UpdateColour(string colourOptionReceivedFromUser)
    {
        switch (colourOptionReceivedFromUser)
        {
            case "yellow":
                selectedColour = "yellow";
                Debug.Log($"Changed selected colour to {selectedColour}.");
                break;
            case "green":
                selectedColour = "green";
                Debug.Log($"Changed selected colour to {selectedColour}.");
                break;
            case "blue":
                selectedColour = "blue";
                Debug.Log($"Changed selected colour to {selectedColour}.");
                break;
            case "red":
                selectedColour = "red";
                Debug.Log($"Changed selected colour to {selectedColour}.");
                break;
            default:
                selectedColour = "yellow";
                Debug.Log($"Which colour was selected is unclear. Defaulting to {selectedColour}.");
                break;
        }
        
        // Finds all colour buttons.
        ColourButtonBehaviour[] colourButtons = FindObjectsOfType<ColourButtonBehaviour>();        
        // Adds/removes the permanent highlighting on all colour buttons.
        foreach (ColourButtonBehaviour colourButton in colourButtons)
        {
            colourButton.UpdatePermanentHighlighting(selectedColour);
        }
    }
}