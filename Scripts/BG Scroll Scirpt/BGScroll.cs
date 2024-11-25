using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGScroll : MonoBehaviour
{
    public float scroll_speed = 0.1f; //controls sped of the space scroll
    public float scroll_duration = 5f;
    private MeshRenderer mesh_Render; //holds the material that we need to use in order to scroll
    private float y_Scroll; //we want to move in the y axis for space
    private float timer; //use this to "stop the game"
    private bool isScrolling = false;
    void Awake()
    {
        mesh_Render = GetComponent<MeshRenderer>(); //grabbing the material 
    }

    void Update()
    {
        if (isScrolling)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime; //decrease timer over time
                Scroll(); //keep scrolling
            }
            else
            {
                isScrolling = false; //stop scrolling when timer reaches 0
            }
        }
    }   

    void Scroll()
    {
        y_Scroll = Time.time * scroll_speed; //Time.time is the time that we've started * the speed of the scroll speed
        Vector2 offset = new Vector2(0f, y_Scroll); //This portion now changes the actual offset of the material or background in the y axis
        mesh_Render.sharedMaterial.SetTextureOffset("_MainTex", offset); //will now actually move the mesh itself
    }

    public void StartBtn()
    {
        if (!isScrolling)
        {
            isScrolling = true;
            timer = scroll_duration;
        }
    }
}
