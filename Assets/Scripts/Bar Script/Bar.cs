using UnityEngine;
using DentedPixel;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public float progressSpeed = 0.05f; // Speed at which the bar progresses
    public float maxScaleX = 1f; // Maximum scale of the progress bar (full progress)
    public CongratScreen CongratScreen;
    public BGScroll bgScroll;
    public TimerScript timerScript;
    private bool isProgressing = false;
    private Coroutine progressCoroutine;

    void Start()
    {
        // Initialize the bar scale to 0
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
    }

    public void StartProgress()
    {
        if (!isProgressing)
        {
            isProgressing = true;
            progressCoroutine = StartCoroutine(ProgressBar());
        }
    }

    public void StopProgress()
    {
        isProgressing = false;
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
            progressCoroutine = null;
        }
    }

    public void ResetBar()
    {
        LeanTween.cancel(bar);
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
        isProgressing = false;
    }

    private System.Collections.IEnumerator ProgressBar()
    {
        while (isProgressing && bar.transform.localScale.x < maxScaleX)
        {
            float newScaleX = bar.transform.localScale.x + progressSpeed * Time.deltaTime;
            newScaleX = Mathf.Min(newScaleX, maxScaleX); // Clamp the scale to max value
            bar.transform.localScale = new Vector3(newScaleX, bar.transform.localScale.y, bar.transform.localScale.z);
            
            if (newScaleX >= maxScaleX)
            {
                CongratScreen.Setup(); // Show the congrats screen
                bgScroll?.StopScrolling();
                timerScript?.PauseTimer();
                isProgressing = false; // Stop the progress
            }

            yield return null; // Wait for the next frame
        }
    }
}
