using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float position_amplifier;
    Vector3 initialPosition;
    private float time_counteract = 0;
    public float rotation_speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 18)
        {
            transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
            time_counteract = -(Time.time);
        }
        else
        {
            transform.position = new Vector3(initialPosition.x + (Time.time + time_counteract) * position_amplifier, (initialPosition.y - (Time.time + time_counteract)) * 0.5f * position_amplifier, 0);
        }
    }
}
