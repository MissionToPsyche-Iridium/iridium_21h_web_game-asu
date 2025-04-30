using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupPanelController : MonoBehaviour
{
    private string sceneToLoad;

    public Button playButton;
    public Button backButton;
    public GameObject popupPanel;
    public GameObject mainDisplay;
    public CameraZoomInZoomOut zoomScript;

    void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); // Hide it at start
        }

        if (playButton != null) playButton.onClick.AddListener(PlayGame);
        if (backButton != null) backButton.onClick.AddListener(BackToMain);
    }


    public void SetScene(string scene)
    {
        sceneToLoad = scene;
        Debug.Log("Scene set to: " + scene);
    }

    public void Show()
    {
        popupPanel.SetActive(true);
        mainDisplay.SetActive(false);
        if (zoomScript != null) zoomScript.ZoomIn();
    }

    void PlayGame()
    {
        Debug.Log("Play button clicked");
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Loading: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty!");
        }
    }


    void BackToMain()
    {
        popupPanel.SetActive(false);
        mainDisplay.SetActive(true);
        if (zoomScript != null) zoomScript.ZoomOut();
    }
}
