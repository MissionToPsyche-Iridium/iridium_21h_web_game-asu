using UnityEngine;

public class camera_collider : MonoBehaviour
{
    private bool keyPressed = true; // Flag to check if the key is pressed
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            keyPressed = true; // Set the flag to true when the key is released
            Debug.Log("Key released. Collision detection is now enabled.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (keyPressed)
        {
            GameObject hitObject = collision.gameObject;
            Debug.Log($"Collision detected with: {hitObject.name}");
        
            // Check if the object has a Renderer component
            Renderer renderer = hitObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Get the color from the material of the Renderer
                Color objectColor = renderer.material.color;

                // Print the color to the console
                Debug.Log($"The color of the object is: {objectColor}");
            }
            else
            {
                Debug.Log("The object does not have a Renderer component.");
            }

             foreach (ContactPoint contact in collision.contacts)
             {
                Debug.Log(contact);
             }

            keyPressed = false;
        }
        else
        {
            Debug.Log("Collision ignored because the key hasn't been pressed.");
        }
    }
}
