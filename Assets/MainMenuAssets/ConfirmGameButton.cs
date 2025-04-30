using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmGameButton : MonoBehaviour
{
    private Button thisButton;
    
    // Set this in the Inspector to either "GammaScene", "Magnetometer", or "Imager"
    public string sceneKey;
    
    // The actual scene name that will be loaded
    private string actualSceneName;

    void Start()
    {
        thisButton = GetComponent<Button>();
        if (thisButton != null)
        {
            thisButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component missing on " + gameObject.name);
        }

        // Map the scene key to the actual scene name.
        if (sceneKey == "GammaScene")
        {
            actualSceneName = "GammaScene";
        }
        else if (sceneKey == "MagScene")
        {
            actualSceneName = "MagScene";
        }
        else if (sceneKey == "Imager")
        {
            actualSceneName = "ImagerGameScene";
        }
        else
        {
            Debug.LogError("Invalid scene key: " + sceneKey);
        }
    }

    private void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(actualSceneName))
        {
            SceneManager.LoadScene(actualSceneName);
        }
        else
        {
            Debug.LogError("Scene to load is not specified!");
        }
    }
}
