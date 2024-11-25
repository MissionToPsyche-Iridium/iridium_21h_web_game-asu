using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer; //the timer itself being set in mainTimer field
    private bool canCount; //chekcs if it's allowed to count
    private bool doOnce; //timer runs once
    private void Start()
    {
        timer = mainTimer;
        uiText.text = timer.ToString("F");
    }

    void Update()
    {
        if (timer >= 0.0f && canCount) //checks if the timer is greater than 0 and if it's allowed to count
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F"); //set the timer into a string
        }

        else if (timer <= 0.0f && !doOnce) //checks if the timer is finished and it's inverse of doOnce, so it'll set to true
        {
            canCount = false;
            doOnce = true; //allows the reset once timer hits 0
            uiText.text = "0.00";
            timer = 0.0f;
        }
    }

    public void ResetBtn() //current situation I want this to only occur once, I might do a "toggle"
    {
        if (doOnce || !canCount) //checks if it hasn't started yet
        {
            timer = mainTimer; //set to the mainTimer in the field
            canCount = true; //checks if the timer starts
            doOnce = false; //check if it runs once
            uiText.text = timer.ToString("F"); // Update UI text
        }
    }
}
