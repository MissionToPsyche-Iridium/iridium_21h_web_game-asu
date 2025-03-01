using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    public Popup Popup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spacecraft"))
        {
            Debug.Log("Player collided with the asteroid!");
            Popup.Setup();
        }
    }
}
