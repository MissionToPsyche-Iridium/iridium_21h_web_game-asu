using UnityEngine;
using UnityEngine.UI;

public class ShowCredit : MonoBehaviour
{
    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();

        if (thisButton != null)
        {
            thisButton.onClick.AddListener(() =>
            {
                Debug.Log("ShowCredit: User clicked credit button.");
                CreditManager.Instance.ShowCredits();
            });
        }
        else
        {
            Debug.LogError("ShowCredit: Button component missing.");
        }
    }
}
