using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SuccessOrFailScript : MonoBehaviour
{
    public FailTimer failTimer;
    public TMP_Text successText;
    private Timer timerScript;
    private Boolean failure = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        successText.fontSize = 0;
    }

    void alterFaildState()
    {
        failure = true;
    }

    Boolean getFailState()
    {
        return failure;
    }

    // Update is called once per frame
    void Update()
    {
        timerScript = FindFirstObjectByType<Timer>();
        failTimer = FindFirstObjectByType<FailTimer>();
        if (timerScript.TimeLeft <= 0 )
        {
            successText.text = "Operation Complete, Established Connection!";
            successText.fontSize = 32;

        }
        if (failTimer.faildetected == true)
        {
            successText.text = "Operation Failed, Please try again!";
            successText.fontSize = 32;
        }
    }
}
