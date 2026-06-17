using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator unitAnimator;

    Vector3 targetPosition;
    GridPosition currentPosition;

    const string MOVE_ANIM = "IsMoving";

    void Awake()
    {
        targetPosition = transform.position;
    }

    void Start()
    {
        currentPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentPosition, this);
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

        GridPosition newPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newPosition != currentPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, currentPosition, newPosition);
            currentPosition = newPosition;
        }
    }


    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
