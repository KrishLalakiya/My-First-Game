using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The object the camera will follow. Assign your Player here.")]
    public Transform target;

    [Header("Settings")]
    [Tooltip("How smoothly the camera catches up to the target. Lower values are slower.")]
    public float smoothSpeed = 0.125f;
    [Tooltip("The offset from the target. Z should be -10 for a 2D game.")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    // LateUpdate is called after all Update functions have been called.
    // This is the best place for camera logic to avoid jittery movement.
    void LateUpdate()
    {
        // Check if a target has been assigned
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate from the camera's current position to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Apply the new position to the camera
            transform.position = smoothedPosition;
        }
    }
}

