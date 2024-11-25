using UnityEngine;
using DentedPixel;
using System;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public int time;
    public Boolean toggle = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public void AnimateBar()
    {
        LeanTween.cancel(bar);
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
        LeanTween.scaleX(bar, 1, time); //progresses the bar toward the full size
    }

    public void StartBtn()
    { 
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z); // Instantly reset to 0
        LeanTween.scaleX(bar, 1, time); //progresses the bar toward the full size
    }

    public void TryagainBtn()
    {
        LeanTween.cancel(bar); //reset bar 
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z); // Instantly reset to 0
        LeanTween.scaleX(bar, 1, time); //progresses the bar toward the full size
    }

}
