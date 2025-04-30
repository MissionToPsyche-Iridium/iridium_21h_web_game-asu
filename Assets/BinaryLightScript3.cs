using UnityEngine;
using System.Collections;
public class BinaryLightScript3 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool colorRed = false;
    private Timer timerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColorAtRandomTime());
    }

    public bool getColor()
    {
        return colorRed;
    }

    private IEnumerator ChangeColorAtRandomTime()
    {
        timerScript = FindFirstObjectByType<Timer>();
        while (colorRed == false)
        {
            float waitTime = Random.Range(3f, 15f);
            yield return new WaitForSeconds(waitTime);

            Color redColor = new Color(255, 0, 0);

            if (spriteRenderer != null)
            {
                spriteRenderer.color = redColor;
                colorRed = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        BinarySwitchScript3 bswitch = FindFirstObjectByType<BinarySwitchScript3>();
        if (timerScript.TimeLeft < 0)
        {
            spriteRenderer.color = new Color(0, 128, 0);
            bswitch.hasbeenclicked = false;
        }
        if (bswitch.hasbeenclicked == true)
        {
            bswitch.hasbeenclicked = false;
            StartCoroutine(ChangeColorAtRandomTime());
        }
    }
}
