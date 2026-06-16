using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator unitAnimator;

    Vector3 targetPosition;

    const string MOVE_ANIM = "IsMoving";

    private void Awake()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        float stopDistane = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistane)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

            unitAnimator.SetBool(MOVE_ANIM, true);
        }
        else
        {
            unitAnimator.SetBool(MOVE_ANIM, false);
        }
    }


    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
