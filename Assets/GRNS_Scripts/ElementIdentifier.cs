using UnityEngine;
using UnityEngine.UI;

public enum ElementType {
    iron,
    nickel,
    si
}

public class ElementIdentifier : MonoBehaviour {
    public ElementType elementType;

    void Start() {
    Image img = GetComponent<Image>();
    Debug.Log($"[ElementIdentifier] {elementType} has type: {elementType} and sprite: {img.sprite.name}");
    }
}
