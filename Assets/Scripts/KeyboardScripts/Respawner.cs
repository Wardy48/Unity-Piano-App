// WHAT THIS SCRIPT IS - This script handles the spawning of rightmost keys, that "disappear" at one or two of the two rightmost "notes sets" because there is no tone that high.
// Thus, this script is attached to two game objects: the parent object of the keys that are not present in notes sets 6 or 7, and to the parent object of the keys that are not present at notes set 7.

using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void SpawnAndDespawnKeysAsNeeded(int notesSetToSpawnOn)
    {
        if (notesSetToSpawnOn <= 6 && transform.CompareTag("Respawn on selectedNotesSet <= 6"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }            
        } else if (notesSetToSpawnOn <= 5 && transform.CompareTag("Respawn on selectedNotesSet <= 5"))
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
