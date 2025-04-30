using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public PlayerController Player;
    public void Setup()
    {
        gameObject.SetActive(true);
        Player?.DisableMovement();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Player?.EnableMovement();
    }
}
