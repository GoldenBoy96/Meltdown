using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Target")]
    public Transform followTransform;

    [Header("Zoom Settings")]
    public float zoomInSize = 4.5f;
    public float zoomOutSize = 6.0f;
    public float zoomSpeed = 3f;

    [Header("Movement Check")]
    public Rigidbody2D playerRb;
    public float movementThreshold = 0.1f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        // Camera follow position
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);

        // Check movement
        float speed = playerRb.velocity.magnitude;
        float targetZoom = speed > movementThreshold ? zoomOutSize : zoomInSize;

        // Smooth zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
}
