using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public bool syllabicNotation;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
