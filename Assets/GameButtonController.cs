using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour
{
    public string sceneToLoad;
    public PopupPanelController popupController;

    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
        if (thisButton != null)
            thisButton.onClick.AddListener(() =>
            {
                popupController.SetScene(sceneToLoad);
                popupController.Show();
            });
    }
}
