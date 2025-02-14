using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class KeysManager : MonoBehaviour
{  
    // ENCAPSULATION. Between here and Start() are examples of me making variables only accessible where they are needed. I have done the same in the other scripts.
    // The "internal" access modifier is not being used, as this is a single project.
    public static KeysManager Instance { get; private set; }
    private Dictionary<KeyCode, GameObject> keyboardToPianoMap;
    public int selectedNotesSet {get; private set; }

    void Awake()
    {
        EnsureExactlyOneInstanceOfThisObjectExists();
    }

    void EnsureExactlyOneInstanceOfThisObjectExists()
    {
        if (Instance == null) {Instance = this;} else {Destroy(gameObject); return;}
    }

    void Start()
    {
        LoadKeysManagerDataFromJSON();
        HandleKeysSpawning();
        CommunicateNotesSelectionToAllTextDisplays();
        MapAllKeysForCorrespondingPianoKeys();
    }

    void HandleKeysSpawning()
    {
        Respawner[] respawners = FindObjectsOfType<Respawner>();
        foreach (Respawner respawner in respawners)
        {
            respawner.SpawnAndDespawnKeysAsNeeded(selectedNotesSet);
        }
    }

    void CommunicateNotesSelectionToAllTextDisplays()
    {
        TextOnNaturalKeys[] textsOnNaturalKeys = FindObjectsOfType<TextOnNaturalKeys>();
        foreach (TextOnNaturalKeys textOnNaturalKey in textsOnNaturalKeys)
        {
            textOnNaturalKey.UpdateText(selectedNotesSet);
        }

        TextOnAccidentalKeys[] textsOnAccidentalKeys = FindObjectsOfType<TextOnAccidentalKeys>();
        foreach (TextOnAccidentalKeys textOnAccidentalKey in textsOnAccidentalKeys)
        {
            textOnAccidentalKey.UpdateText(selectedNotesSet);
        }
    }

    void MapAllKeysForCorrespondingPianoKeys()
    {
        // Remember, future me: if it has a hyphen, then it's a game object, NOT an audio source!
        keyboardToPianoMap = new Dictionary<KeyCode, GameObject>
        {
            // Naturals
            [KeyCode.Q] = GameObject.Find("A0 - La0"),
            [KeyCode.W] = GameObject.Find("B0 - Si0"),
            [KeyCode.E] = GameObject.Find("C0 - Do1"),
            [KeyCode.R] = GameObject.Find("D0 - Re1"),
            [KeyCode.T] = GameObject.Find("E0 - Mi1"),
            [KeyCode.Y] = GameObject.Find("F0 - Fa1"),
            [KeyCode.U] = GameObject.Find("G0 - Sol1"),
            [KeyCode.I] = GameObject.Find("A1 - La1"),
            [KeyCode.O] = GameObject.Find("B1 - Si1"),
            [KeyCode.P] = GameObject.Find("C1 - Do2"),
            [KeyCode.LeftBracket] = GameObject.Find("D1 - Re2"),
            [KeyCode.RightBracket] = GameObject.Find("E1 - Mi2"),
            [KeyCode.LeftShift] = GameObject.Find("F1 - Fa2"),
            [KeyCode.Z] = GameObject.Find("G1 - Sol2"),
            [KeyCode.X] = GameObject.Find("A2 - La2"),
            [KeyCode.C] = GameObject.Find("B2 - Si2"),
            [KeyCode.V] = GameObject.Find("C2 - Do3"),
            [KeyCode.B] = GameObject.Find("D2 - Re3"),
            [KeyCode.N] = GameObject.Find("E2 - Mi3"),
            [KeyCode.M] = GameObject.Find("F2 - Fa3"),
            [KeyCode.Comma] = GameObject.Find("G2 - Sol3"),
            [KeyCode.Period] = GameObject.Find("A3 - La3"),
            [KeyCode.Slash] = GameObject.Find("B3 - Si3"),

            // Accidentals
            [key: KeyCode.Alpha2] = GameObject.Find("A#0 - La#0"),
            [KeyCode.Alpha4] = GameObject.Find("C#0 - Do#1"),
            [KeyCode.Alpha5] = GameObject.Find("D#0 - Re#1"),
            [KeyCode.Alpha7] = GameObject.Find("F#0 - Fa#1"),
            [KeyCode.Alpha8] = GameObject.Find("G#0 - Sol#1"),
            [KeyCode.Alpha9] = GameObject.Find("A#1 - La#1"),
            [KeyCode.Minus] = GameObject.Find("C#1 - Do#2"),
            [KeyCode.Equals] = GameObject.Find("D#1 - Re#2"),
            [KeyCode.A] = GameObject.Find("F#1 - Fa#2"),
            [KeyCode.S] = GameObject.Find("G#1 - Sol#2"),
            [KeyCode.D] = GameObject.Find("A#2 - La#2"),
            [KeyCode.G] = GameObject.Find("C#2 - Do#3"),
            [KeyCode.H] = GameObject.Find("D#2 - Re#3"),
            [KeyCode.K] = GameObject.Find("F#2 - Fa#3"),
            [KeyCode.L] = GameObject.Find("G#2 - Sol#3"),
            [KeyCode.Semicolon] = GameObject.Find("A#3 - La#3")
        };
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.StopAllSounds();
            selectedNotesSet = ShiftNotesInputLeft();
            Debug.Log($"Selected notes set: {selectedNotesSet}");
            HandleKeysSpawning();
            CommunicateNotesSelectionToAllTextDisplays();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.StopAllSounds();
            selectedNotesSet = ShiftNotesInputRight();
            Debug.Log($"Selected notes set: {selectedNotesSet}");
            HandleKeysSpawning();
            CommunicateNotesSelectionToAllTextDisplays();
        }

        // Check if any of the keys in the Dictionary have been pressed
        foreach (var dictionaryEntry in keyboardToPianoMap)
        {
            if (Input.GetKeyDown(dictionaryEntry.Key))
            {
                Debug.Log("Pressing piano key corresponding to " + dictionaryEntry.Key);
                PlayPianoKey(dictionaryEntry.Value);
            } else if (Input.GetKeyUp(dictionaryEntry.Key))
            {
                Debug.Log("No longer pressing piano key corresponding to " + dictionaryEntry.Key);
                LetGoOfPianoKey(dictionaryEntry.Value);
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
        if(selectedNotesSet < 5)
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
        
        if (gameObject.TryGetComponent<KeyBehaviour>(out var keyBehaviour))
        {
            keyBehaviour.PlayTone(selectedNotesSet, MainManager.Instance.selectedColour);
        }
        else
        {
            Debug.Log("No KeyBehaviour script found on " + gameObject.name);
        }
    }

    private void LetGoOfPianoKey(GameObject gameObject)
    {
        
        if (gameObject.TryGetComponent<KeyBehaviour>(out var keyBehaviour))
        {
            keyBehaviour.StopTone(selectedNotesSet);
        }
        else
        {
            Debug.Log("No KeyBehaviour script found on " + gameObject.name);
        }
    }

    // Data persistence across sections from this point forward
    [System.Serializable]
    class SaveData
    {
        public int selectedNotesSetJSON;
    }

    public void SaveKeysManagerDataToJSON()
    {
        SaveData data = new SaveData();
        data.selectedNotesSetJSON = selectedNotesSet;

        string json = JsonUtility.ToJson(data);  
        File.WriteAllText(Application.persistentDataPath + "/keysManagerData.json", json);
    }
    public void LoadKeysManagerDataFromJSON()
    {
        string path = Application.persistentDataPath + "/keysManagerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            selectedNotesSet = data.selectedNotesSetJSON;
        } else
        {
            selectedNotesSet = 3;
            Debug.Log("The selected notes set was not loaded from JSON, either due to an error or because it does not exist. Defaulted to set three.");
        }
    }
    
    // IMPORTANT: OnApplicationQuit() may not be called in time on mobile.
    void OnApplicationQuit()
    {
        SaveKeysManagerDataToJSON();
    }

    // OnDestroy() is called when "stopping play mode" on the Unity Editor, or closing the scene.
    void OnDestroy()
    {
        LoadKeysManagerDataFromJSON();
    }
}