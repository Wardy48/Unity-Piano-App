// IMPORTANT: I deleted the unnecessary default namespaces that are created in every new C# script automatically
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private int selectedNotesSet;
    private AudioSource audioSource0;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;
    private AudioSource audioSource4;
    private AudioSource audioSource5;
    private AudioSource audioSource6;
    private AudioSource audioSource7;
    private AudioSource[] allAudioSources;

    private Color originalSpriteColour;
    private Color originalColour;
    private SpriteRenderer spriteRenderer;
    private GameObject gameManager;
    private ItalianKeysToPiano italianKeysToPiano;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        italianKeysToPiano = gameManager.GetComponent<ItalianKeysToPiano>();
        selectedNotesSet = italianKeysToPiano.SelectedNotesSet;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpriteColour = spriteRenderer.color;

        Renderer renderer = GetComponent<Renderer>();
        originalColour = renderer.material.color;

        // Get the AudioSource components attached to the GameObject
        // I made this array on the basis of this online post, which explains that they map to components on the object from top to bottom: https://gamedev.stackexchange.com/questions/63818/how-do-i-have-multiple-audio-sources-on-a-single-object-in-unity
        // Each one of these audio sources has a note applied, from the lowest possible to the highest possible for how the keyboard is set up.
        allAudioSources = GetComponents<AudioSource>();
        audioSource0 = allAudioSources[0];
        audioSource1 = allAudioSources[1];
        audioSource2 = allAudioSources[2];
        audioSource3 = allAudioSources[3];
        audioSource4 = allAudioSources[4];
        audioSource5 = allAudioSources[5];
        audioSource6 = allAudioSources[6];
        audioSource7 = allAudioSources[7];
        // The sound clips for the piano will be courtesy of the University of Iowa: https://theremin.music.uiowa.edu/MISpiano.html
        GrabSelectedNotesSetFromItalianKeysToPiano();
    }

    public int GrabSelectedNotesSetFromItalianKeysToPiano()
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
    
    public void PlayTone(int selectedNotesSet)
    {
        if (allAudioSources[selectedNotesSet] != null && allAudioSources[selectedNotesSet].enabled)
        {
            Renderer renderer = GetComponent<Renderer>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if(spriteRenderer.color.r == 0 && spriteRenderer.color.g == 0 && spriteRenderer.color.b == 0)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f);
            }    
            renderer.material.color = new Color(0.96f, 0.96f, 0.86f);
            allAudioSources[selectedNotesSet].Play();
            Debug.Log("Right now, " + allAudioSources[selectedNotesSet] + " should be playing.");
        }
        else
        {
            Debug.Log("No AudioSource component to play on this game object: " + gameObject.name);
        }
    }

    public void StopTone()
    {
        Renderer renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer.material.color = originalColour;
        spriteRenderer.color = originalSpriteColour;

        if (audioSource0 != null)
        {
            audioSource0.Stop();
            Debug.Log("Right now, " + audioSource0.name + " should be stopping.");
        }
        else if (audioSource1 != null)
        {
            audioSource1.Stop();
            Debug.Log("Right now, " + audioSource1.name + " should be stopping.");
        }
        else if (audioSource2 != null)
        {
            audioSource2.Stop();
            Debug.Log("Right now, " + audioSource2.name + " should be stopping.");
        }
        else if (audioSource3 != null)
        {
            audioSource3.Stop();
            Debug.Log("Right now, " + audioSource3.name + " should be stopping.");
        }
        else if (audioSource4 != null)
        {
            audioSource4.Stop();
            Debug.Log("Right now, " + audioSource4.name + " should be stopping.");
        }
        else if (audioSource5 != null)
        {
            audioSource5.Stop();
            Debug.Log("Right now, " + audioSource5.name + " should be stopping.");
        }
        else if (audioSource6 != null)
        {
            audioSource6.Stop();
            Debug.Log("Right now, " + audioSource6.name + " should be stopping.");
        }
        else if (audioSource7 != null)
        {
            audioSource7.Stop();
            Debug.Log("Right now, " + audioSource7.name + " should be stopping.");
        }
        else
        {
            Debug.Log("No AudioSource component to stop on this game object: " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was successfully clicked, and could sense it. But only because it has a collider.");
        PlayTone(selectedNotesSet);
    }
    void OnMouseUp()
    {
        Debug.Log(gameObject.name + " was successfully released from the mouse click.");
        StopTone();
    }
}