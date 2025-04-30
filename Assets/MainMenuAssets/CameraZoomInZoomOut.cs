using UnityEngine;
using System.Collections;

public class CameraZoomInZoomOut : MonoBehaviour
{
    public Camera mainCamera;
    private Transform targetPoint;   

    // Zoom parameters
    public float zoomedSize = 3.7f;     
    public float zoomSpeed = 2f;      

    private float defaultSize;      
    private Vector3 defaultPosition; 

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        defaultSize = mainCamera.orthographicSize;
        defaultPosition = mainCamera.transform.position;

        // Create a new GameObject and use its transform as the targetPoint.
        GameObject targetObj = new GameObject("TargetPoint");
        targetPoint = targetObj.transform;
        targetPoint.position = new Vector3(4f, 1f, defaultPosition.z);
    }

    public void ZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomToTarget(targetPoint.position, zoomedSize));
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomToTarget(defaultPosition, defaultSize));
    }

    private IEnumerator ZoomToTarget(Vector3 targetPosition, float targetSize)
    {
        float t = 0f;
        Vector3 startPosition = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
    }
}
