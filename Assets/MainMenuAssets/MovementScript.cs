using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float amplifier;
    public float frequency;
    Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, Mathf.Sin(Time.time * frequency) * amplifier + initialPosition.y, 0);
    }
}
