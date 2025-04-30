using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public static CreditManager Instance;

    public GameObject PopUpCredit;
    public bool isCreditOpen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, remove if not needed
        }
        else
        {
            Destroy(gameObject);
        }

        if (PopUpCredit != null)
        {
            PopUpCredit.SetActive(false); // 🔒 ensures it starts hidden
            Debug.Log("CreditManager: PopUpCredit set to inactive on start.");
        }
        else
        {
            Debug.LogError("CreditManager: PopUpCredit not assigned in Inspector.");
        }
    }

    public void ShowCredits()
    {
        if (!isCreditOpen && PopUpCredit != null)
        {
            PopUpCredit.SetActive(true);
            isCreditOpen = true;
            Debug.Log("CreditManager: Showing credits.");
        }
    }

    public void HideCredits()
    {
        if (isCreditOpen && PopUpCredit != null)
        {
            PopUpCredit.SetActive(false);
            isCreditOpen = false;
            Debug.Log("CreditManager: Hiding credits.");
        }
    }
}
