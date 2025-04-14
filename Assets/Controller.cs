using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int xAxisAngle = 0;
    public TMP_Text x_text;
    public TMP_Text y_text;
    public GameObject color_model2;
    public Button reset;
    public float rotationSpeed = 5f; // Adjust rotation speed
    private Vector3 lastMousePosition; // Tracks the mouse position
    private bool objectSelected = false;

    void Start()
    {
        color_model2.SetActive(true);
        // add a listener for the reset button
        reset.onClick.AddListener(resetRotation);
    }

    void Update()
    {
        if (objectSelected)
            rotateAsteroid();
    }

    // public void rotateAsteroid()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         lastMousePosition = Input.mousePosition;
    //     }
    //     else if (Input.GetMouseButton(0))
    //     {
    //         // Calculate the mouse movement delta
    //         Vector3 delta = Input.mousePosition - lastMousePosition;

    //         // Adjust sensitivity for smoother rotation
    //         float rotationX = delta.y * rotationSpeed * Time.deltaTime;
    //         float rotationY = -delta.x * rotationSpeed * Time.deltaTime;

    //         Vector3 currentRotation = color_model2.transform.rotation.eulerAngles;
            
    //         // Apply the incremental rotations directly to the transform
    //         color_model2.transform.Rotate(Vector3.right, rotationX, Space.World);
    //         color_model2.transform.Rotate(Vector3.up, rotationY, Space.World);

    //         // Update the last mouse position
    //         lastMousePosition = Input.mousePosition;

    //         // Retrieve the rotation from the object's transform
    //         Vector3 worldRotation = color_model2.transform.rotation.eulerAngles;

    //         // Update the displayed angles
    //         x_text.text = $"{Mathf.Round(worldRotation.x)}";
    //         y_text.text = $"{Mathf.Round(worldRotation.y)}";
    //     }
    // }

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
        x_text.text = "0";
        y_text.text = "0";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        objectSelected = false;
    }
}
