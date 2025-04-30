using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
        if (thisButton != null)
            thisButton.onClick.AddListener(ReturnToMainMenu);
        else
            Debug.LogError("MainMenuButtonController: Button component missing on " + gameObject.name);
    }

    void ReturnToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("MainMenuButtonController: Main Menu scene name is not specified!");
        }
    }
}
