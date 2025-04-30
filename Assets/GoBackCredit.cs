using UnityEngine;
using UnityEngine.UI;

public class GoBackCredit : MonoBehaviour
{
    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();

        if (thisButton != null)
        {
            thisButton.onClick.AddListener(() =>
            {
                Debug.Log("GoBackCredit: User clicked go back.");
                CreditManager.Instance.HideCredits();
            });
        }
        else
        {
            Debug.LogError("GoBackCredit: Button component missing.");
        }
    }
}
