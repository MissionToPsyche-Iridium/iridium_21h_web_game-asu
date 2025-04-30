using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{

    public Button ConfirmButton;
    public Button DenyButton;
    private Vector3 initialScale;
    public float scale_change_amplifier;
    private float time_from_start;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time_from_start = Time.time;
        initialScale = transform.localScale;
        transform.localScale = new Vector3(0f, 0f, initialScale.z);
        
    }

    private void OnEnable()
    {
        time_from_start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.2f)
        {
            transform.localScale = new Vector3((Time.time - time_from_start) * scale_change_amplifier, (Time.time - time_from_start) * scale_change_amplifier, initialScale.z);
        }
        if (transform.localScale.x >= 0.2f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, initialScale.z);
        }
    }
}
