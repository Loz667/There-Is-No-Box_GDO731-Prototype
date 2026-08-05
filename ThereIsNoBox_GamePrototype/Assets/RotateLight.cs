using UnityEngine;

public class RotateLight : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
