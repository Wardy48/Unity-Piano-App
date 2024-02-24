using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    private int selectedNotesSet;
    // Start is called before the first frame update
    void Start()
    {
        GrabSelectedNotesSetFromItalianKeysToPiano();
        RespawnNeededKeys();
    }

    // Update is called once per frame
    void Update()
    {
        GrabSelectedNotesSetFromItalianKeysToPiano();
        RespawnNeededKeys();
    }
    private int GrabSelectedNotesSetFromItalianKeysToPiano()
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
    void RespawnNeededKeys()
    {
        if (selectedNotesSet <= 6 && transform.CompareTag("Respawn on selectedNotesSet <= 6"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }            
        } else if (selectedNotesSet <= 5 && transform.CompareTag("Respawn on selectedNotesSet <= 5"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }              
        } else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }                          
        }
    }
}
