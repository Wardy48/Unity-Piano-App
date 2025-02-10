using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    // I made this array on the basis of the following online post, which explains that they map to components on the object from top to bottom: https://gamedev.stackexchange.com/questions/63818/how-do-i-have-multiple-audio-sources-on-a-single-object-in-unity
    private AudioSource[] allAudioSources;
    private Color originalSpriteColour;
    private Color originalColour;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {   
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
        Renderer renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.color.r == 0 && spriteRenderer.color.g == 0 && spriteRenderer.color.b == 0)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f);
        }    
        renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
    }

    public void ChangeKeyColourBecauseTheKeyIsReleased()
    {
        Renderer renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer.material.color = originalColour;
        spriteRenderer.color = originalSpriteColour;
    }

    public void PlayTone(int notesSetToUse)
    {
        ChangeKeyColourBecauseTheKeyIsBeingPressed();

        if (allAudioSources[notesSetToUse] != null)
        {
            allAudioSources[notesSetToUse].Play();
            Debug.Log("Right now, " + allAudioSources[notesSetToUse].clip.name + " should be playing. If there is no sound, the audio clip may not have been added to the audio source component.");
        }
        else
        {
            Debug.Log("No AudioSource component to play on this game object: " + gameObject.name);
        }
    }

    public void StopTone(int notesSetToUse)
    {

        ChangeKeyColourBecauseTheKeyIsReleased();

        if (allAudioSources[notesSetToUse] != null)
        {
            allAudioSources[notesSetToUse].Stop();
            Debug.Log("Right now, " + allAudioSources[notesSetToUse].clip.name + " should be stopping.");
        }
        else
        {
            Debug.Log("No AudioSource component to stop on this game object: " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was successfully clicked with the mouse, and could sense it. Because it has a collider.");
        PlayTone(KeysManager.Instance.selectedNotesSet);
    }
    void OnMouseUp()
    {
        Debug.Log(gameObject.name + " was successfully released from the mouse click.");
        StopTone(KeysManager.Instance.selectedNotesSet);
    }
}