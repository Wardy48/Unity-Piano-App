using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{
    internal bool syllabicNotation;
    internal string selectedColour { get; private set; }
    internal bool requiresTextOnWhiteKeysToBeTurnedWhite { get; private set; }
    internal bool requiresTextOnBlackKeysToBeTurnedBlack { get; private set; }
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
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene changes, by adding a listener.
        } else 
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu Screen")
        {
            Debug.Log("The Main Manager sensed we are in the 'Menu Screen'");
            UpdateColour(selectedColour);
        }
    }

    // TODO: also ensure the text on the keys remains readable despite the highlighting, which can involve letting it temporarily change.

    void Start()
    {
        LoadMainManagerDataFromJSON();
        UpdateColour(selectedColour);
    }

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

    [System.Serializable]
    class SaveData
    {
        public string selectedColourJSON;
    }

    public void SaveMainManagerDataToJSON()
    {
        SaveData data = new SaveData();
        data.selectedColourJSON = selectedColour;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/mainManagerData.json", json);
    }

    public void LoadMainManagerDataFromJSON()
    {
        string path = Application.persistentDataPath + "/mainManagerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            selectedColour = data.selectedColourJSON;
        } else
        {
            selectedColour = "yellow";
            Debug.Log("The selected colour was not loaded from JSON, either due to an error or because it does not exist. Defaulted to yellow.");
        }
    }

    // IMPORTANT: OnApplicationQuit() may not be called in time on mobile.
    void OnApplicationQuit()
    {
        SaveMainManagerDataToJSON();
    }

    // OnDestroy() is called when "stopping play mode" on the Unity Editor, or closing the scene.
    void OnDestroy()
    {
        LoadMainManagerDataFromJSON();
    }
}