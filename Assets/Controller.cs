using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int xAxisAngle = 0;
    public TMP_Text x_text;
    public TMP_Text y_text;
    public GameObject color_model2;
    public GameObject pBar0, pBar1, pBar2, pBar3;
    public GameObject utilityCanvas, returnHome;
    public GameObject introCanvas, instrCanvas1, instrCanvas2, instrCanvas3;
    public GameObject task1, task1TryAgain, task2, task2TryAgain, task3, task3TryAgain, done;
    public Button task1Forward, task1TryAgainForward, task2Forward, task2TryAgainForward;
    public Button task3Forward, task3TryAgainForward, doneHome, donePlayAgain;
    public Button reset, home, keepPlaying, returnHomeBtn;
    public Button introForward, instr1Forward, instr1Back, instr2Forward, instr2Back, instr3Forward, instr3Back;
    public float rotationSpeed = 5f; // Adjust rotation speed
    private Vector3 lastMousePosition; // Tracks the mouse position
    private bool objectSelected = false;
    private bool restorePsyche = false;
    private bool restoreCamera = false;
    private int taskTracker = 0;
    public GameObject cameraTarget;
    bool UIActive = false;

    void Start()
    {
        color_model2.SetActive(false);
        introCanvas.SetActive(true);
        // add a listener for the reset button
        reset.onClick.AddListener(resetRotation);
        home.onClick.AddListener(() => returnHomePopUp(true));
        keepPlaying.onClick.AddListener(() => returnHomePopUp(false));
        addListeners();
    }

    void Update()
    {
        if (objectSelected)
            rotateAsteroid();
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the active state
            cameraTarget.SetActive(!cameraTarget.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            // only check the picture if the camera is on
            if (cameraTarget.activeSelf)
                checkPicture();
        }
    }

 public void rotateAsteroid()
{
    if (Input.GetMouseButtonDown(0))
    {
        lastMousePosition = Input.mousePosition;
    }
    else if (Input.GetMouseButton(0))
    {
        // Calculate the mouse movement delta
        Vector3 delta = Input.mousePosition - lastMousePosition;

        // Adjust sensitivity for smoother rotation (and invert the direction of the Y-axis if needed)
        float rotationX = delta.y * rotationSpeed * Time.deltaTime;  // Y movement = X rotation
        float rotationY = -delta.x * rotationSpeed * Time.deltaTime; // X movement = Y rotation

        // Apply rotation around the Y-axis (world space) for horizontal dragging
        color_model2.transform.Rotate(Vector3.up, rotationY, Space.World);

        // Check if Y rotation is in the range of 90 to 270 degrees (inverted axis range)
        float currentYRotation = color_model2.transform.localEulerAngles.y;
        
        // If the Y-axis rotation is between 90 and 270, reverse the X-axis rotation direction
        if (currentYRotation > 90f && currentYRotation < 270f)
        {
            rotationX = -rotationX; // Reverse the direction of rotation for X
        }

        // Apply rotation around the X-axis (local space) for vertical dragging
        // Clamp the X rotation to prevent it from flipping upside down
        float newRotationX = color_model2.transform.localEulerAngles.x + rotationX;

        // Convert to [-180, 180] range and then clamp to [-90, 90]
        if (newRotationX > 180f) newRotationX -= 360f; // Convert to [-180, 180] range
        newRotationX = Mathf.Clamp(newRotationX, -90f, 90f); // Prevent flipping beyond the set limits

        // Apply the new clamped X rotation
        color_model2.transform.localEulerAngles = new Vector3(newRotationX, color_model2.transform.localEulerAngles.y, 0f);

        // Update the last mouse position
        lastMousePosition = Input.mousePosition;

        // Retrieve the rotation from the object's transform (in world space)
        Quaternion currentRotation = color_model2.transform.rotation;

        // Convert the quaternion rotation to Euler angles (in world space)
        Vector3 worldRotation = currentRotation.eulerAngles;

        // Update the displayed angles
        x_text.text = $"{Mathf.Round(worldRotation.x)}";
        y_text.text = $"{Mathf.Round(worldRotation.y)}";
    }
}

    public void resetRotation()
    {
        // set the rotation to the identity quaternion (no rotation)
        color_model2.transform.rotation = Quaternion.identity;

        // update the text fields
        x_text.text = "000";
        y_text.text = "000";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        objectSelected = false;
    }

    public void addListeners() 
    {
        introForward.onClick.AddListener(() => switchCanvasView(introCanvas, instrCanvas1));
        instr1Forward.onClick.AddListener(() => switchCanvasView(instrCanvas1, instrCanvas2));
        instr1Back.onClick.AddListener(() => switchCanvasView(instrCanvas1, introCanvas));
        instr2Forward.onClick.AddListener(() => switchCanvasView(instrCanvas2, instrCanvas3));
        instr2Back.onClick.AddListener(() => switchCanvasView(instrCanvas2, instrCanvas1));
        instr3Forward.onClick.AddListener(() => switchCanvasView(instrCanvas3, task1));
        instr3Back.onClick.AddListener(() => switchCanvasView(instrCanvas3, instrCanvas2));
        task1Forward.onClick.AddListener(() => turnCanvasOff(task1, false, true));
        task1TryAgainForward.onClick.AddListener(() => turnCanvasOff(task1TryAgain, true, false));
        task2Forward.onClick.AddListener(() => turnCanvasOff(task2, true, false));
        task2TryAgainForward.onClick.AddListener(() => turnCanvasOff(task2TryAgain, true, false));
        task3Forward.onClick.AddListener(() => turnCanvasOff(task3, true, false));
        task3TryAgainForward.onClick.AddListener(() => turnCanvasOff(task3TryAgain, true, false));
        donePlayAgain.onClick.AddListener(startOver);

    }

    public void switchCanvasView(GameObject canvas1, GameObject canvas2) 
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }

    public void turnCanvasOff(GameObject canvas, bool turnCamOn, bool turnPBarOn)
    {
        canvas.SetActive(false);
        // set game features to active
        color_model2.SetActive(true);
        utilityCanvas.SetActive(true);
        
        if (turnCamOn)
            cameraTarget.SetActive(true);

        if (turnPBarOn)
            pBar0.SetActive(true);
    }

    public void checkPicture() 
    {
        // use the rotation text
        int x = Int32.Parse(x_text.text);
        int y = Int32.Parse(y_text.text);

        // task 1
        if (taskTracker == 0)
        {
            if (((y >= 240) && (y <= 310)) || ((y >= 58) && (y <= 127)))
            {
                taskTracker = 1;
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                pBar0.SetActive(false);
                pBar1.SetActive(true);
                task2.SetActive(true);
            }
            else
            {
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                task1TryAgain.SetActive(true);
            }
        }
        // task 2
        else if (taskTracker == 1)
        {
            if (((y >= 329) ||  (y <= 35)) && ((x <= 1) || (x >= 345))) 
            {
                taskTracker = 2;
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                pBar1.SetActive(false);
                pBar2.SetActive(true);
                task3.SetActive(true);
            }
            else 
            {
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                task2TryAgain.SetActive(true);
                UIActive = true;
            }
        }
        else if (taskTracker == 2)
        {
            if (((y >= 170) &&  (y <= 190)) && ((x >= 0) && (x <= 20))) 
            {
                taskTracker = 3;
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                pBar2.SetActive(false);
                pBar3.SetActive(true);
                done.SetActive(true);
            }
            else 
            {
                color_model2.SetActive(false);
                //utilityCanvas.interactable(false);
                cameraTarget.SetActive(false);
                task3TryAgain.SetActive(true);
            }
        }
    }

    public void startOver() 
    {
        taskTracker = 0;
        done.SetActive(false);
        task1.SetActive(true);
        pBar0.SetActive(true);
        pBar1.SetActive(false);
        pBar2.SetActive(false);
        pBar3.SetActive(false);
    }

    public void returnHomePopUp(bool yes) 
    {
        if (yes)
        {
            SceneManager.LoadScene("MainMenu");

            /*if (color_model2.activeSelf)
                restorePsyche = true;

            if (cameraTarget.activeSelf)
                restoreCamera = true;

            color_model2.SetActive(false);
            cameraTarget.SetActive(false);
            home.enabled = false;
            reset.enabled = false;
            returnHome.SetActive(true);
        */}
        else 
        {
            if (restorePsyche)
            {
                color_model2.SetActive(true);
                restorePsyche = false;
            }

            if (restoreCamera)
            {
                cameraTarget.SetActive(true);
                restoreCamera = false;
            }
            home.enabled = true;
            reset.enabled = true;
            returnHome.SetActive(false);
        }
    }
}
