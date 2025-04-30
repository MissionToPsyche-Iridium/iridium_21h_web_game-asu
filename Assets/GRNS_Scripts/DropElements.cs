using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropElements : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject[] elementPrefabs;       // Iron, Nickel, Silicate prefabs
    public GameObject[] DropArea;             // 20 center slots
    public Transform dropManager;             // Parent object for user elements (e.g. DropManager)

    private Dictionary<GameObject, ElementType> expectedElements = new Dictionary<GameObject, ElementType>();
    private GameObject currentDraggedObject;
    private Vector3 originalPosition;

    void Start()
    {
        AssignRandomElements();
        CreateUserElements();
    }

    void AssignRandomElements()
    {
        expectedElements.Clear();
        Debug.Log("------ Assigning Random Elements ------");

        for (int i = 0; i < DropArea.Length; i++)
        {
            int randomIndex = Random.Range(0, elementPrefabs.Length);
            GameObject selectedPrefab = elementPrefabs[randomIndex];
            Sprite sprite = selectedPrefab.GetComponent<Image>().sprite;
            ElementType type = selectedPrefab.GetComponent<ElementIdentifier>().elementType;

            Image dropImage = DropArea[i].GetComponent<Image>();
            dropImage.sprite = sprite;
            expectedElements[DropArea[i]] = type;

            Debug.Log($"[AssignRandomElements] Image {i}: Assigned {type} with sprite {sprite.name}");
        }
    }

    void CreateUserElements()
    {
        foreach (GameObject prefab in elementPrefabs)
        {
            GameObject instance = Instantiate(prefab, dropManager);
            instance.transform.localScale = Vector3.one;
            instance.AddComponent<CanvasGroup>();  // Useful for drag transparency
            instance.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentDraggedObject = eventData.pointerDrag;
        if (currentDraggedObject != null)
        {
            originalPosition = currentDraggedObject.transform.position;
            currentDraggedObject.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentDraggedObject != null)
        {
            float scaleFactor = FindFirstObjectByType<Canvas>().scaleFactor;
            currentDraggedObject.transform.position += (Vector3)(eventData.delta / scaleFactor);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentDraggedObject != null)
        {
            currentDraggedObject.GetComponent<Image>().raycastTarget = true;
            currentDraggedObject.transform.position = originalPosition;
            currentDraggedObject = null;
        }
    }

    public void HandleDrop(GameObject userElement, GameObject dropAreaObj)
    {
        if (userElement == null || dropAreaObj == null) return;

        ElementType userType = userElement.GetComponent<ElementIdentifier>().elementType;
        ElementType expectedType = expectedElements[dropAreaObj];

        Debug.Log($"[HandleDrop] User: {userType}, Expected: {expectedType}");

        if (userType == expectedType)
        {
            Debug.Log("[HandleDrop] Correct match!");
            ReplaceMatchedElement(dropAreaObj);
            FindFirstObjectByType<GameManager>().UpdateProgress(true);
        }
        else
        {
            Debug.Log("[HandleDrop] Wrong match.");
            userElement.transform.position = originalPosition;
            FindFirstObjectByType<GameManager>().UpdateProgress(false);
        }
    }

    void ReplaceMatchedElement(GameObject dropAreaObj)
    {
        int index;
        ElementType newType;
        Sprite newSprite;

        do
        {
            index = Random.Range(0, elementPrefabs.Length);
            newType = elementPrefabs[index].GetComponent<ElementIdentifier>().elementType;
        }
        while (newType == expectedElements[dropAreaObj]);  // Avoid same type

        newSprite = elementPrefabs[index].GetComponent<Image>().sprite;
        dropAreaObj.GetComponent<Image>().sprite = newSprite;
        expectedElements[dropAreaObj] = newType;

        Debug.Log($"[ReplaceMatchedElement] New type = {newType}, sprite = {newSprite.name}");
    }
}
