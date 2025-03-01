using UnityEngine;

public class Popup : MonoBehaviour
{
    public PlayerController Player;
    public BGScroll BGScroll;
    public Bar Bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup()
    {
        gameObject.SetActive(true);
        Player?.DisableMovement();
        BGScroll.PauseScrolling();
        Bar.PauseProgress();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Player?.EnableMovement();
        Bar.ContinueProgress();
    }
}
