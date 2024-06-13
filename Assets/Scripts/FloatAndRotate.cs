using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Speed of rotation
    public float floatSpeed = 0.5f;    // Speed of floating up and down
    public float floatAmplitude = 0.5f; // Amplitude of the floating motion

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Store the starting position and rotation of the object
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Calculate the new Y position for floating motion
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Apply the new position while keeping the original X and Z positions
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Keep the object upright
        transform.rotation = Quaternion.Euler(startRotation.eulerAngles.x, transform.rotation.eulerAngles.y, startRotation.eulerAngles.z);
    }
}
