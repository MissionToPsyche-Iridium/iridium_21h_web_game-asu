using UnityEngine;

public class BinarySwitchScript4 : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer;
    private BinaryLightScript4 light;
    public bool hasbeenclicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasbeenclicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        print("Binary Sprite 1 Clicked");
        light = FindFirstObjectByType<BinaryLightScript4>();
        if (light.colorRed == true)
        {
            print("Binary Light 1 is red");
            targetSpriteRenderer.color = new Color(0, 128, 0);
            light.colorRed = false;
            hasbeenclicked = true;
        }
    }
}
