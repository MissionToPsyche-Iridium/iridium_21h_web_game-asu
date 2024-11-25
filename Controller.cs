using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public int xAxisAngle = 0;
    public Text xText;
    public GameObject psyche_0_0a; 
    public GameObject psyche_90_0a;
    public GameObject psyche_180_0a;
    public Button xUp;
    public Button xDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide each view of the asteroid except for (0,0)
        psyche_90_0a.SetActive(false);
        psyche_180_0a.SetActive(false);
        // Add listeners of the buttons
        xUp.onClick.AddListener(delegate {changeXAxis(90); });
        xDown.onClick.AddListener(delegate {changeXAxis(-90); });

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void changeXAxis(int angleToAdd) 
    {
        int newAngle = xAxisAngle + angleToAdd;
        // only update the angle if it is within certain boundries
        if ((newAngle >= 0) && (newAngle <= 180)) 
        {
            xAxisAngle = newAngle;
        }
        
        switchMap();
        xText.text = "(" + xAxisAngle.ToString();
    }

    public void switchMap() 
    {
        if (xAxisAngle == 0) {
            psyche_0_0a.SetActive(true);
            psyche_90_0a.SetActive(false);
            psyche_180_0a.SetActive(false);
        } 
        else if (xAxisAngle == 90) {
            psyche_0_0a.SetActive(false);
            psyche_90_0a.SetActive(true);
            psyche_180_0a.SetActive(false);
        } 
        else {
            psyche_0_0a.SetActive(false);
            psyche_90_0a.SetActive(false);
            psyche_180_0a.SetActive(true);
        }
    }
}
