using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    public bool isGrabbing = false;
    public Transform grabbedObject;
    public Rigidbody grabbedRigidbody;
    public Collider playerCollider; // Collider of the player

    public float grabRange = 5f;

    public Camera playerCamera;

    void Start()
    {
        // Find the camera in the scene. Adjust this line if the camera is not a child.
        playerCamera = GetComponentInChildren<Camera>();
        playerCollider = GetComponent<Collider>(); // Assuming this script is on the player GameObject
        if (playerCamera == null)
        {
            Debug.LogError("uh camera aint there bro");
        }
        if (playerCollider == null)
        {
            Debug.LogError("where da player at");
        }
    }

    void Update()
    {
        if (playerCamera == null || playerCollider == null) return;

        if (Input.GetMouseButton(0)) // While holding the mouse button
        {
            if (!isGrabbing) // Start grabbing if not already
            {
                TryGrabObject();
            }
        }
        else
        {
            if (isGrabbing) // Drop the object when the mouse button is released
            {
                DropObject();
            }
        }

        if (isGrabbing)
        {
            UpdateGrabbedObjectPosition();
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                GrabObject(hit.transform);
            }
        }
    }

    void GrabObject(Transform objectToGrab)
    {
        isGrabbing = true;
        grabbedObject = objectToGrab;
        grabbedRigidbody = objectToGrab.GetComponent<Rigidbody>();

        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.velocity = Vector3.zero; // Stop any existing velocity
            grabbedRigidbody.angularVelocity = Vector3.zero; // Stop any existing angular velocity
            grabbedRigidbody.useGravity = false; // Disable gravity while holding

            // Disable collision between player and the grabbed object
            Collider grabbedCollider = grabbedObject.GetComponent<Collider>();
            if (grabbedCollider != null)
            {
                Physics.IgnoreCollision(grabbedCollider, playerCollider, true);
            }
        }
    }

    void DropObject()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.useGravity = true; // Re-enable gravity when dropped
            grabbedRigidbody.velocity = Vector3.zero; // Ensure no manual forces affect the object
            grabbedRigidbody.angularVelocity = Vector3.zero; // Reset any rotational force

            // Re-enable collision between player and the dropped object
            Collider grabbedCollider = grabbedObject.GetComponent<Collider>();
            if (grabbedCollider != null)
            {
                Physics.IgnoreCollision(grabbedCollider, playerCollider, false);
            }
        }

        isGrabbing = false;
        grabbedObject = null;
        grabbedRigidbody = null; // Clear the reference to the Rigidbody
    }

void UpdateGrabbedObjectPosition()
{
    if (grabbedObject != null)
    {
        // Calculate the desired new position
        Vector3 newPosition = playerCamera.transform.position + playerCamera.transform.forward * 3.5f;

        // Cast a ray from the current position to the new position to check for collisions
        RaycastHit hit;
        Vector3 direction = newPosition - grabbedObject.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(grabbedObject.position, direction, out hit, distance))
        {
            // If a collision is detected, move the object to the point of collision minus a small offset
            newPosition = hit.point - direction.normalized * 0.7f;
        }

        // Smoothly interpolate the object's position to the new (or adjusted) position
        grabbedObject.position = Vector3.Lerp(grabbedObject.position, newPosition, Time.deltaTime * 10f);
    }
}
}
