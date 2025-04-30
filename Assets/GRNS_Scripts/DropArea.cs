using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler {
    private DropElements dropElements;
    
    void Start() {
        dropElements = Object.FindFirstObjectByType<DropElements>();
    }
    
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Dropped onto: " + gameObject.name);
        if (dropElements != null) {
            dropElements.HandleDrop(eventData.pointerDrag, gameObject);
        } else {
            Debug.LogError("DropElements script not found in scene!");
        }
    }
}
