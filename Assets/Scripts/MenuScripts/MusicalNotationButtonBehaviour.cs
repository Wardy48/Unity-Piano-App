// IMPORTANT: I deleted the unnecessary default namespaces that are created in every new C# script automatically
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicalNotationButtonBehaviour : MonoBehaviour
{
    private bool syllabicNotationIsSetByThisButton;
    public void OnButtonClick()
    {
        if (this.gameObject.name == "Syllabic_Button")
        {
            syllabicNotationIsSetByThisButton = true;
        } else if (this.gameObject.name == "Alphabetical_Button")
        {
            syllabicNotationIsSetByThisButton = false;
        } else
        {
            Debug.Log("There was a problem determining what notation to use. Please check the code. Syllabic has been chosen as it's the default");
            syllabicNotationIsSetByThisButton = true;
        }
        
        CommunicateNotationToMainManager();
        
        SceneManager.LoadScene("Keyboard");
    }
    private void CommunicateNotationToMainManager()
    {
        GameObject mainManager = GameObject.Find("MainManager");
        MainManager mainManagerScript = mainManager.GetComponent<MainManager>();
        mainManagerScript.syllabicNotation = syllabicNotationIsSetByThisButton;
    }
}
