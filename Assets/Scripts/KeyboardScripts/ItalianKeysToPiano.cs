// IMPORTANT: I deleted the unnecessary default namespaces that are created in every new C# script automatically
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItalianKeysToPiano : MonoBehaviour
{  
    // ENCAPSULATION. Between here and Start() are examples of me making variables only accessible where they are needed. I have done the same in the other scripts.
    private Dictionary<KeyCode, GameObject> keyboardToPianoMap;
    private int selectedNotesSet;
    public int SelectedNotesSet
    {
        get { return selectedNotesSet; }
    }
    [SerializeField] GameObject naturals;
    [SerializeField] GameObject accidentals;
    [SerializeField] GameObject reappearOn6;
    [SerializeField] GameObject reappearOn6Too;
    [SerializeField] GameObject reappearOn5;
    [SerializeField] GameObject reappearOn5Too;


    void Start()
    {
        LoadNotesSelection();
        CommunicateNotesSelectionToAllTextDisplays();
        MapAllKeysForCorrespondingPianoKeys();
    }

    void CommunicateNotesSelectionToAllTextDisplays()
    {
        TextOnNaturalKeys[] textsOnNaturalKeys = FindObjectsOfType<TextOnNaturalKeys>();
        foreach (TextOnNaturalKeys textOnNaturalKey in textsOnNaturalKeys)
        {
            textOnNaturalKey.UpdateText();
        }

        TextOnAccidentalKeys[] textsOnAccidentalKeys = FindObjectsOfType<TextOnAccidentalKeys>();
        foreach (TextOnAccidentalKeys textOnAccidentalKey in textsOnAccidentalKeys)
        {
            textOnAccidentalKey.UpdateText();
        }
    }

    void MapAllKeysForCorrespondingPianoKeys()
    {
        // Initialize the Dictionary
        keyboardToPianoMap = new Dictionary<KeyCode, GameObject>();

        // Assign a KeyCode to each GameObject
        // As a rule of thumb for future me: if it has a hyphen, then it's a gameObject, NOT an audio source!

        // Naturals
        keyboardToPianoMap[KeyCode.Q] = GameObject.Find("A0 - La0");
        keyboardToPianoMap[KeyCode.W] = GameObject.Find("B0 - Si0");
        keyboardToPianoMap[KeyCode.E] = GameObject.Find("C0 - Do1");
        keyboardToPianoMap[KeyCode.R] = GameObject.Find("D0 - Re1");
        keyboardToPianoMap[KeyCode.T] = GameObject.Find("E0 - Mi1");
        keyboardToPianoMap[KeyCode.Y] = GameObject.Find("F0 - Fa1");
        keyboardToPianoMap[KeyCode.U] = GameObject.Find("G0 - Sol1");
        keyboardToPianoMap[KeyCode.I] = GameObject.Find("A1 - La1");
        keyboardToPianoMap[KeyCode.O] = GameObject.Find("B1 - Si1");
        keyboardToPianoMap[KeyCode.P] = GameObject.Find("C1 - Do2");
        keyboardToPianoMap[KeyCode.LeftBracket] = GameObject.Find("D1 - Re2");
        keyboardToPianoMap[KeyCode.RightBracket] = GameObject.Find("E1 - Mi2");
        keyboardToPianoMap[KeyCode.LeftShift] = GameObject.Find("F1 - Fa2");
        keyboardToPianoMap[KeyCode.Z] = GameObject.Find("G1 - Sol2");
        keyboardToPianoMap[KeyCode.X] = GameObject.Find("A2 - La2");
        keyboardToPianoMap[KeyCode.C] = GameObject.Find("B2 - Si2");
        keyboardToPianoMap[KeyCode.V] = GameObject.Find("C2 - Do3");
        keyboardToPianoMap[KeyCode.B] = GameObject.Find("D2 - Re3");
        keyboardToPianoMap[KeyCode.N] = GameObject.Find("E2 - Mi3");
        keyboardToPianoMap[KeyCode.M] = GameObject.Find("F2 - Fa3");
        keyboardToPianoMap[KeyCode.Comma] = GameObject.Find("G2 - Sol3");
        keyboardToPianoMap[KeyCode.Period] = GameObject.Find("A3 - La3");
        keyboardToPianoMap[KeyCode.Slash] = GameObject.Find("B3 - Si3");

        // Accidentals
        keyboardToPianoMap[key: KeyCode.Alpha2] = GameObject.Find("A#0 - La#0");
        keyboardToPianoMap[KeyCode.Alpha4] = GameObject.Find("C#0 - Do#1");
        keyboardToPianoMap[KeyCode.Alpha5] = GameObject.Find("D#0 - Re#1");
        keyboardToPianoMap[KeyCode.Alpha7] = GameObject.Find("F#0 - Fa#1");
        keyboardToPianoMap[KeyCode.Alpha8] = GameObject.Find("G#0 - Sol#1");
        keyboardToPianoMap[KeyCode.Alpha9] = GameObject.Find("A#1 - La#1");
        keyboardToPianoMap[KeyCode.Minus] = GameObject.Find("C#1 - Do#2");
        keyboardToPianoMap[KeyCode.Equals] = GameObject.Find("D#1 - Re#2");
        keyboardToPianoMap[KeyCode.A] = GameObject.Find("F#1 - Fa#2");
        keyboardToPianoMap[KeyCode.S] = GameObject.Find("G#1 - Sol#2");
        keyboardToPianoMap[KeyCode.D] = GameObject.Find("A#2 - La#2");
        keyboardToPianoMap[KeyCode.G] = GameObject.Find("C#2 - Do#3");
        keyboardToPianoMap[KeyCode.H] = GameObject.Find("D#2 - Re#3");
        keyboardToPianoMap[KeyCode.K] = GameObject.Find("F#2 - Fa#3");
        keyboardToPianoMap[KeyCode.L] = GameObject.Find("G#2 - Sol#3");
        keyboardToPianoMap[KeyCode.Semicolon] = GameObject.Find("A#3 - La#3");
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedNotesSet = ShiftNotesInputLeft();
            Debug.Log(selectedNotesSet);
            CommunicateNotesSelectionToAllTextDisplays();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedNotesSet = ShiftNotesInputRight();
            Debug.Log(selectedNotesSet);
            CommunicateNotesSelectionToAllTextDisplays();
        }

        // Check if any of the keys in the Dictionary have been pressed
        foreach (var entry in keyboardToPianoMap)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                Debug.Log("Pressing piano key corresponding to " + entry.Key);
                // If the key has been pressed, perform an action on the corresponding GameObject
                PlayPianoKey(entry.Value);
            } else if (Input.GetKeyUp(entry.Key))
            {
                Debug.Log("No longer pressing piano key corresponding to " + entry.Key);
                LetGoOfPianoKey(entry.Value);
            }
        }
    }

    private int ShiftNotesInputLeft()
    {
        if(selectedNotesSet != 0)
        {
            return selectedNotesSet - 1;
        } else
        {
            Debug.Log("Can't go any lower!");
            return selectedNotesSet;
        }
    }

    private int ShiftNotesInputRight()
    {
        if(selectedNotesSet < 7)
        {
            return selectedNotesSet + 1;
        } else
        {
            Debug.Log("Can't go any higher!");
            return selectedNotesSet;
        }
    }

    private void PlayPianoKey(GameObject gameObject)
    {
        KeyBehaviour keyBehaviour = gameObject.GetComponent<KeyBehaviour>();

        if (keyBehaviour != null)
        {
            keyBehaviour.PlayTone(selectedNotesSet);
        }
        else
        {
            Debug.Log("No KeyBehaviour script found on " + gameObject.name);
        }
    }
    private void LetGoOfPianoKey(GameObject gameObject)
    {
        KeyBehaviour keyBehaviour = gameObject.GetComponent<KeyBehaviour>();

        if (keyBehaviour != null)
        {
            keyBehaviour.StopTone();
        }
        else
        {
            Debug.Log("No KeyBehaviour script found on " + gameObject.name);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int selectedNotesSetToStoreInJSON;
    }

    public void SaveNotesSelection()
    {
        SaveData data = new SaveData();
        data.selectedNotesSetToStoreInJSON = selectedNotesSet;

        string json = JsonUtility.ToJson(data);  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadNotesSelection()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            selectedNotesSet = data.selectedNotesSetToStoreInJSON;
        } else
        {
            selectedNotesSet = 3;
        }
    }
    
    // IMPORTANT: OnApplicationQuit() may not be called in time on mobile.
    void OnApplicationQuit()
    {
        SaveNotesSelection();
    }

    // OnDestroy() is called when "stopping play mode" on the Unity Editor, or closing the scene.
    void OnDestroy()
    {
        SaveNotesSelection();
    }
}