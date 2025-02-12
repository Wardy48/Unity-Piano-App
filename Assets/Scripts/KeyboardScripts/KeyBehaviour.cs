using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyBehaviour : MonoBehaviour
{
    // I made this array on the basis of the following online post, which explains that they map to components on the object from top to bottom: https://gamedev.stackexchange.com/questions/63818/how-do-i-have-multiple-audio-sources-on-a-single-object-in-unity
    private AudioSource[] allAudioSources;
    private Color originalSpriteColour;
    private Color originalColour;
    private SpriteRenderer spriteRenderer;
    private string colourToUseForTheKeys;
    TextOnNaturalKeys textOnNaturalKeysScript;
    TextOnAccidentalKeys textOnAccidentalKeysScript;
    bool isKeyNatural;

    // Start is called before the first frame update
    void Start()
    {
        // Get the script of the text component of the key, be it natural or accidental.
        if (transform.GetChild(0).GetChild(0).GetComponent<TextOnNaturalKeys>() != null)
        {
            textOnNaturalKeysScript = transform.GetChild(0).GetChild(0).GetComponent<TextOnNaturalKeys>();
            isKeyNatural = true;
        } else if (transform.GetChild(0).GetChild(0).GetComponent<TextOnAccidentalKeys>() != null)
        {
            textOnAccidentalKeysScript = transform.GetChild(0).GetChild(0).GetComponent<TextOnAccidentalKeys>();
            isKeyNatural = false;
        } else
        {
            Debug.LogWarning("A key's KeyBehaviour script couldn't reference the text component of that key.");
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpriteColour = spriteRenderer.color;

        Renderer renderer = GetComponent<Renderer>();
        originalColour = renderer.material.color;

        // Each one of these audio sources has a note applied, from the lowest possible to the highest possible tone for how the keyboard is set up (depending on how far on the keyboard the user has scrolled).
        allAudioSources = GetComponents<AudioSource>();
        // The sound clips for the piano will be courtesy of the University of Iowa: https://theremin.music.uiowa.edu/MISpiano.html
        // Or at least, of a person I found on GitHub that curated the uni's notes for people like me.
    }
    
    public void ChangeKeyColourBecauseTheKeyIsBeingPressed()
    {        
        // This renderer is specialised for 2D sprites.
        spriteRenderer = GetComponent<SpriteRenderer>();

        bool keyIsBlack = spriteRenderer.color.r == 0 && spriteRenderer.color.g == 0 && spriteRenderer.color.b == 0;
        
        if(keyIsBlack)
        {
            // Changes the key to white, only for the time the sprite colour is being applied.
            spriteRenderer.color = Color.white;
        }

        // This is the more core renderer.
        Renderer renderer = GetComponent<Renderer>();
        
        // Choose which colour to use on the core renderer.
        if (MainManager.Instance != null && MainManager.Instance.selectedColour != null)
        {
            colourToUseForTheKeys = MainManager.Instance.selectedColour;
            switch (colourToUseForTheKeys)
            {
                case "yellow":
                    renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
                    break;
                case "green":
                    renderer.material.color = new Color(0.077f, 0.377f, 0.079f);
                    break;
                case "blue":
                    renderer.material.color = new Color(0.036f, 0.041f, 0.453f);
                    break;
                case "red":
                    renderer.material.color = new Color(0.736f, 0.101f, 0.22f);
                    break;
                default:
                    renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
                    Debug.Log("Colour not recognised. Defaulted to yellow.");
                    break;
            }
            Debug.Log($"Changing the colour of the selected key to {colourToUseForTheKeys}.");
        } else if (MainManager.Instance != null)
        {
            renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
            Debug.Log("The Main Manager instance was found, but its selected colour for keys' selection was null. Defaulted to yellow.");
        } else
        {
            renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
            Debug.Log("No Main Manager instance found. Defaulted to yellow highlighting of selected keys.");
        }
    }

    public void ChangeKeyColourBecauseTheKeyIsReleased()
    {
        Renderer renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer.material.color = originalColour;
        spriteRenderer.color = originalSpriteColour;
    }

    public void PlayTone(int notesSetToUse, string keyColourSetting)
    {
        // Play the tone
        if (allAudioSources[notesSetToUse] != null)
        {
            allAudioSources[notesSetToUse].Play();
            Debug.Log("Right now, " + allAudioSources[notesSetToUse].clip.name + " should be playing. If there is no sound, the audio clip may not have been added to the audio source component.");
        }
        else
        {
            Debug.Log("No AudioSource component to play on this game object: " + gameObject.name);
        }
        
        ChangeKeyColourBecauseTheKeyIsBeingPressed();
        
        // Change the colour of the key texts as needed
        if (textOnNaturalKeysScript != null)
        {
            textOnNaturalKeysScript.ChangeTextColourIfNeeded(isKeyNatural, keyColourSetting);
        } else if (textOnAccidentalKeysScript != null) {
            textOnAccidentalKeysScript.ChangeTextColourIfNeeded(isKeyNatural, keyColourSetting);
        } else
        {
            Debug.Log("Cannot see whether the key's text's colour should be changed.");
        }

    }

    public void StopTone(int notesSetToUse)
    {
        if (allAudioSources[notesSetToUse] != null)
        {
            allAudioSources[notesSetToUse].Stop();
            Debug.Log("Right now, " + allAudioSources[notesSetToUse].clip.name + " should be stopping.");
        }
        else
        {
            Debug.Log("No AudioSource component to stop on this game object: " + gameObject.name);
        }

        ChangeKeyColourBecauseTheKeyIsReleased();

        // Set text colour back to normal
        if (textOnNaturalKeysScript != null)
        {
            textOnNaturalKeysScript.SetTextColourBackToNormal(isKeyNatural);
        } else if (textOnAccidentalKeysScript != null)
        {
            textOnAccidentalKeysScript.SetTextColourBackToNormal(isKeyNatural);
        }
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was successfully clicked with the mouse, and could sense it. Because it has a collider.");
        PlayTone(KeysManager.Instance.selectedNotesSet, MainManager.Instance.selectedColour);
    }
    void OnMouseUp()
    {
        Debug.Log(gameObject.name + " was successfully released from the mouse click.");
        StopTone(KeysManager.Instance.selectedNotesSet);
    }
}