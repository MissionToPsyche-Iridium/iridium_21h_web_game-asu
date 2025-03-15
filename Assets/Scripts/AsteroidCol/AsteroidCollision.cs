using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    //public Popup Popup;
    public PopupManager popupManager;
    private bool hasCollided = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollided && collision.CompareTag("Spacecraft"))
        {
            hasCollided = true;
            Debug.Log("Player collided with the asteroid!");
            //Popup.Setup();
            popupManager.ShowRandomPopup();
        }
    }

    public void ResetCollisionState()
    {
        hasCollided = false;
    }
}
