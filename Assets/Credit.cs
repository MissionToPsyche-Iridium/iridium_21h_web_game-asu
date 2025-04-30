using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    // Reference to your Credit Panel (the entire popup that contains credit text and back button)
    public GameObject PopUpCredit;

    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
        if(thisButton != null)
            thisButton.onClick.AddListener(ShowCreditPanel);
        else
            Debug.LogError("Credit: Button component missing.");
    }

    void ShowCreditPanel()
    {
        if (PopUpCredit != null)
            PopUpCredit.SetActive(true);
    }
}
