using UnityEngine;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    public PlayerController Player;

    void Start()
    {
        // Since the screen is active by default, disable movement on start
        Player?.DisableMovement();
    }
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
