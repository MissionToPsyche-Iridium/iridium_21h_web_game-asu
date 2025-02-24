using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float min_X, max_X;
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 30f;

    // Floating motion parameters
    public float floatAmplitudeHorizontal = 0.1f;
    public float floatAmplitudeVertical = 0.05f;
    public float floatFrequencyHorizontal = 1f;
    public float floatFrequencyVertical = 1.5f;

    private Quaternion targetRotation;
    private Vector3 originalPosition;
    private Vector3 basePosition;

    public Bar progressBar; // Reference to the progress bar script

    private bool canMove = true;

    void Start()
    {
        targetRotation = transform.rotation;
        originalPosition = transform.position;
        basePosition = originalPosition;
    }

    void Update()
    {
        if(canMove)
        {
            MovePlayer();
            RotatePlayer();
        }
        ApplyFloatingMotion();
    }

    public void DisableMovement()
    {
        canMove = false;
        // Reset rotation to neutral when movement is disabled
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Vector3 temp = basePosition;
            temp.x += speed * Time.deltaTime;

            if (temp.x > max_X)
            {
                temp.x = max_X;
            }

            basePosition = temp;
            targetRotation = Quaternion.Euler(0, 0, -maxRotationAngle);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Vector3 temp = basePosition;
            temp.x -= speed * Time.deltaTime;

            if (temp.x < min_X)
            {
                temp.x = min_X;
            }

            basePosition = temp;
            targetRotation = Quaternion.Euler(0, 0, maxRotationAngle);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void ApplyFloatingMotion()
    {
        float horizontalOffset = Mathf.Sin(Time.time * floatFrequencyHorizontal) * floatAmplitudeHorizontal;
        float verticalOffset = Mathf.Sin(Time.time * floatFrequencyVertical) * floatAmplitudeVertical;

        Vector3 newPosition = basePosition;
        newPosition.x += horizontalOffset;
        newPosition.y += verticalOffset;

        transform.position = newPosition;
    }

    void RotatePlayer()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SineWave"))
        {
            Debug.Log("Player entered sine wave area.");
            progressBar?.StartProgress();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SineWave"))
        {
            Debug.Log("Player exited sine wave area.");
            progressBar?.StopProgress();
        }
    }

}