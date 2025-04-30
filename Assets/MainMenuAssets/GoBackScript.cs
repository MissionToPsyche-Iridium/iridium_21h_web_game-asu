using UnityEngine;
using UnityEngine.UI;

public class GoBackS : MonoBehaviour
{
    private Button thisButton;
    private CameraZoomInZoomOut zoomInZoomOut;

    private GameObject parentGameSelectionCanvas;
    private GameObject gammaRayButton;
    private GameObject magnetometerButton;
    private GameObject imagerButton;
    private GameObject creditsButton;

    private GameObject parentCanvas;
    private GameObject targetPanel;

    void Start()
    {
        // Find the "GameSelectionCanvas" (the parent for your game buttons)
        parentGameSelectionCanvas = GameObject.Find("GameSelectionCanvas");
        if (parentGameSelectionCanvas == null)
        {
            Debug.LogError("GameSelectionCanvas not found. Check your Hierarchy for the correct name.");
            return;
        }

        // Find each button by name under "GameSelectionCanvas"
        gammaRayButton = parentGameSelectionCanvas.transform.Find("GammaRayButton")?.gameObject;
        magnetometerButton = parentGameSelectionCanvas.transform.Find("MagnetometerButton")?.gameObject;
        imagerButton = parentGameSelectionCanvas.transform.Find("ImagerButton")?.gameObject;
        creditsButton = parentGameSelectionCanvas.transform.Find("CreditsButton")?.gameObject;

        // Log an error if any of these are missing
        if (gammaRayButton == null)
            Debug.LogError("GammaRayButton not found under GameSelectionCanvas.");
        if (magnetometerButton == null)
            Debug.LogError("MagnetometerButton not found under GameSelectionCanvas.");
        if (imagerButton == null)
            Debug.LogError("ImagerButton not found under GameSelectionCanvas.");
        if (creditsButton == null)
            Debug.LogError("CreditsButton not found under GameSelectionCanvas.");

        // Find camera zoom script in the scene
        zoomInZoomOut = Object.FindFirstObjectByType<CameraZoomInZoomOut>();
        if (zoomInZoomOut == null)
            Debug.LogError("CameraZoomInZoomOut script not found. Attach it to your main camera or another object.");

        // Hook up the current button's OnClick
        thisButton = GetComponent<Button>();
        if (thisButton != null)
            thisButton.onClick.AddListener(OnButtonClick);
        else
            Debug.LogError("No Button component found on this GameObject.");

        // Find the pop-up canvas and the pop-up panel
        parentCanvas = GameObject.Find("PopUpCanvas");
        if (parentCanvas == null)
        {
            Debug.LogError("PopUpCanvas not found in the scene.");
            return;
        }

        // Make sure the pop-up panel is a child of PopUpCanvas
        Transform panelTransform = parentCanvas.transform.Find("PopUpPanel");
        if (panelTransform != null)
            targetPanel = panelTransform.gameObject;
        else
            Debug.LogError("PopUpPanel not found under PopUpCanvas.");

        // Optional checks for null
        if (targetPanel == null)
            Debug.LogError("targetPanel is null in GoBackScript.");
    }

    private void OnButtonClick()
    {
        // Scale down the pop-up
        if (targetPanel != null)
            targetPanel.transform.localScale = new Vector3(0f, 0f, 1f);

        // Zoom out the camera
        if (zoomInZoomOut != null)
            zoomInZoomOut.ZoomOut();

        // Re-enable the main selection buttons
        if (gammaRayButton != null) gammaRayButton.SetActive(true);
        if (magnetometerButton != null) magnetometerButton.SetActive(true);
        if (imagerButton != null) imagerButton.SetActive(true);
        if (creditsButton != null) creditsButton.SetActive(true);

        // Hide the pop-up panel
        if (targetPanel != null)
            targetPanel.SetActive(false);
    }
}
