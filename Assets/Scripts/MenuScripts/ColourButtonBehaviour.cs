using UnityEngine;

public class ColourButtonBehaviour : MonoBehaviour
{
    [SerializeField] Transform highlightingOfThisGameObject;
    [SerializeField] Transform highlightingOfThisGameObjectWhenItsColourIsSelected;
    [SerializeField] string colourSelectedWhenPressingThisButton;
    MainManager mainManager;
    
    void OnMouseEnter()
    {
        Debug.Log($"{this} is being hovered over.");
        highlightingOfThisGameObject.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        highlightingOfThisGameObject.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        mainManager = FindObjectOfType<MainManager>();
        mainManager.UpdateColour(colourSelectedWhenPressingThisButton);
    }

    internal void UpdatePermanentHighlighting(string colourTheButtonIsBeingInformedAbout)
    {
        if (colourTheButtonIsBeingInformedAbout == colourSelectedWhenPressingThisButton)
        {
            highlightingOfThisGameObjectWhenItsColourIsSelected.gameObject.SetActive(true);
        } else
        {
            highlightingOfThisGameObjectWhenItsColourIsSelected.gameObject.SetActive(false);
        }
    }
}
