using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BroomSnap : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public Transform attachPoint;
    private FloatAndRotate floatAndRotate;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        floatAndRotate = GetComponent<FloatAndRotate>();

        if (attachPoint == null)
        {
            attachPoint = new GameObject("Attach Point").transform;
            attachPoint.SetParent(transform, false);
        }

        grabInteractable.attachTransform = attachPoint;
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (floatAndRotate != null)
        {
            floatAndRotate.enabled = false;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (floatAndRotate != null)
        {
            floatAndRotate.enabled = true;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
