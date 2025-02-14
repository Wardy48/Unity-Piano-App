// WHAT THIS SCRIPT IS - This script handles the spawning of rightmost keys, that "disappear" at the rightmost "notes set" because there is no tone that high for them.
// Thus, this script is attached to the parent of notes that can despawn on the final notes set. One parent for naturals and one parent for accidentals.

using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void SpawnAndDespawnKeysAsNeeded(int notesSetToSpawnOn)
    {
        if (notesSetToSpawnOn == 5)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }            
        } else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }                          
        }
    }
}
