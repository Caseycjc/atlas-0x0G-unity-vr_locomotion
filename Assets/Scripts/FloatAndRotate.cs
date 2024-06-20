using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Speed of rotation
    public float floatSpeed = 0.5f;    // Speed of floating up and down
    public float floatAmplitude = 0.5f; // Amplitude of the floating motion

    private Vector3 dropPosition;
    private Quaternion dropRotation;
    private bool isDropped = false;

    void Start()
    {
        // Store the initial position and rotation
        dropPosition = transform.position;
        dropRotation = transform.rotation;
    }

    void Update()
    {
        if (isDropped)
        {
            // Rotate the object around its Y-axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Calculate the new Y position for floating motion
            float newY = dropPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

            // Apply the new position while keeping the original X and Z positions
            transform.position = new Vector3(dropPosition.x, newY, dropPosition.z);

            // Keep the object upright
            transform.rotation = Quaternion.Euler(dropRotation.eulerAngles.x, transform.rotation.eulerAngles.y, dropRotation.eulerAngles.z);
        }
    }

    public void Drop()
    {
        // Set the current position and rotation as the drop position
        dropPosition = transform.position;
        dropRotation = transform.rotation;
        isDropped = true;

        // Enable Rigidbody physics if needed
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void Grab()
    {
        isDropped = false;

        // Disable Rigidbody physics if needed
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
