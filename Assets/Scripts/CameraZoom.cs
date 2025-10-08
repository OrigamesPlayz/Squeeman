using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float minFOV = 17.5f;   // Smallest zoom (most zoomed in)
    public float maxFOV = 90f;     // Largest zoom (most zoomed out)
    public float baseZoomFOV = 30f; // Default zoom FOV when pressing C
    public float normalFOV = 60f;  // Camera FOV when not zooming

    [Header("Speed Settings")]
    public float scrollSpeed = 10f;  // How sensitive the scroll wheel is
    public float smoothSpeed = 5f;   // How smoothly the FOV changes

    private float targetFOV;   // The value we want to move toward
    private bool isZooming;    // Whether C is being held down

    void Start()
    {
        targetFOV = normalFOV;
        Camera.main.fieldOfView = normalFOV;
    }

    void Update()
    {
        // Detect when C is pressed or released
        if (Input.GetKeyDown(KeyCode.C))
        {
            isZooming = true;
            targetFOV = baseZoomFOV; // Start zoom at 30 FOV
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isZooming = false;
            targetFOV = normalFOV; // Return to normal view
        }

        // Only allow scrolling if zooming
        if (isZooming)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            targetFOV -= scrollInput * scrollSpeed;
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        }

        // Smoothly move toward the target FOV
        Camera.main.fieldOfView = Mathf.Lerp(
            Camera.main.fieldOfView,
            targetFOV,
            Time.deltaTime * smoothSpeed
        );
    }
}
