using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] Transform hitVFX;

    Vector3 targetPosition;

    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        float moveSpeed = 100f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;

            trail.transform.parent = null;

            Destroy(gameObject);

            Instantiate(hitVFX, targetPosition, Quaternion.identity);
        }
    }
}
