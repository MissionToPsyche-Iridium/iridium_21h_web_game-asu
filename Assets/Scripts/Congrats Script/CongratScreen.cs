using UnityEngine;
using UnityEngine.UI;

public class CongratScreen : MonoBehaviour{
   
    public PlayerController Player;
    public Bar Bar;
    public void Setup()
    {
        gameObject.SetActive(true);
        Player?.DisableMovement();
        Bar.PauseProgress();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Player?.EnableMovement();
        Bar.ContinueProgress();
    }
}
