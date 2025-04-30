using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = true;
    public TMP_Text timeText;
    private BinaryLightScript1 ls1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        FailTimer failTimer = FindFirstObjectByType<FailTimer>();
        if (TimerOn && failTimer.faildetected == false)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimerOn = false;
                timeText.text = "0:00";
            }
        }
    }

    void updateTimer(float currentTime)
    {
        float minutes;
        float seconds;
        if (currentTime > 0)
        {
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else
        {
            //ls1 = FindObjectOfType<BinaryLightScript1>();
            //ls1.colorRed = false;
            //ls1.spriteRenderer.color = new Color(0, 128, 0);
            timeText.text = "0:00"; 
        }
    }

    string getText()
    {
        return timeText.text;
    }
}
