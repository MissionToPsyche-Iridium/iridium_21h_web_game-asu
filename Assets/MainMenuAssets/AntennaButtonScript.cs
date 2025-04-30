using UnityEngine;
using UnityEngine.UI;

public class AntennaButtonScript : MonoBehaviour
{
    GameObject parentCanvas;
    GameObject targetPanel;
    GameObject myCamera;
    GameObject mySelf;
    GameObject creditsButton;
    private Button thisButton;
    public CameraZoomInZoomOut zoomInZoomOut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoomInZoomOut = FindObjectOfType<CameraZoomInZoomOut>();
        Debug.Log("Found zoomInZoomOut: " + zoomInZoomOut);
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnButtonClick);

        creditsButton = GameObject.Find("CreditsButton");
        Debug.Log("Found creditsButton: " + creditsButton);

        parentCanvas = GameObject.Find("PopUpCanvas");
        Debug.Log("Found parentCanvas: " + parentCanvas);
        
        if (parentCanvas != null)
        {
            targetPanel = parentCanvas.transform.Find("PopUpPanel").gameObject;
            Debug.Log("Found targetPanel: " + targetPanel);
        }
        else
        {
            Debug.LogError("parentCanvas not found");
        }

        mySelf = GameObject.Find("AntennaButton");
        Debug.Log("Found mySelf (AntennaButton): " + mySelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonClick()
    {
        targetPanel.SetActive(true);
        creditsButton.SetActive(false);
        zoomInZoomOut.ZoomIn();
        mySelf.SetActive(false);
    }
}
