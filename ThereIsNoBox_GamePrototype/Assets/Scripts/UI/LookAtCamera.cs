using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] bool invert;

    Transform camTransform;

    void Awake()
    {
        camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (invert)
        {
            Vector3 directionToCamera = (camTransform.position- transform.position).normalized;
            transform.LookAt(transform.position + directionToCamera * -1);
        }
        else
        {
            transform.LookAt(camTransform);
        }
    }
}
