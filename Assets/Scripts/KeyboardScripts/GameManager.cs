using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject naturals;
    [SerializeField] GameObject accidentals;
    [SerializeField] GameObject leftmostKeyText;
    [SerializeField] GameObject keyBindingsCanvas;
    [SerializeField] GameObject defaultKeyboardCanvas;
    [SerializeField] GameObject separatorLine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ToggleTextOnKeys(naturals.transform);
            ToggleTextOnKeys(accidentals.transform);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleSeparatorLine();
        }
    }

    void ToggleTextOnKeys(Transform parent)
    {
        TextOnNaturalKeys textOnNaturalKeys = leftmostKeyText.GetComponent<TextOnNaturalKeys>();
        bool alphabeticalNotation = textOnNaturalKeys.alphabeticalNotation;
        for (int i = 0; i < parent.childCount; i++)
        {
            if(!parent.GetChild(i).tag.Contains("Respawn"))
            {
                Transform key = parent.GetChild(i);
                if(alphabeticalNotation)
                {ToggleIfNotLabelledLa(key);}
                else{ToggleIfNotLabelledDo(key);}
            } else
            {
                ToggleTextOnKeys(parent.GetChild(i));
            }
        }
    }

    void ToggleIfNotLabelledLa(Transform child)
    {
        Transform canvasOfTheKey = child.GetChild(0);
       if ((!child.CompareTag("La")) && canvasOfTheKey.gameObject.activeSelf)
       {
            canvasOfTheKey.gameObject.SetActive(false);
       } else if (!child.CompareTag("La"))
       {
            canvasOfTheKey.gameObject.SetActive(true);
       }
    }

    void ToggleIfNotLabelledDo(Transform child)
    {
        Transform canvasOfTheKey = child.GetChild(0);
       if ((!child.CompareTag("Do")) && canvasOfTheKey.gameObject.activeSelf)
       {
            canvasOfTheKey.gameObject.SetActive(false);
       } else if (!child.CompareTag("Do"))
       {
            canvasOfTheKey.gameObject.SetActive(true);
       }
    }

    public void LoadMenuScreenScene()
    {
        SceneManager.LoadScene("Menu Screen");
    }

    public void LoadKeyBindingsCanvas()
    {
        keyBindingsCanvas.SetActive(true);
        defaultKeyboardCanvas.SetActive(false);
    }

    public void LoadDefaultKeyboardCanvas()
    {
        defaultKeyboardCanvas.SetActive(true);
        keyBindingsCanvas.SetActive(false);
    }

    public void ToggleSeparatorLine()
    {
        if (!separatorLine.activeSelf)
        {
            separatorLine.SetActive(value: true);
        } else
        {
            separatorLine.SetActive(value: false);
        }
    }
}
