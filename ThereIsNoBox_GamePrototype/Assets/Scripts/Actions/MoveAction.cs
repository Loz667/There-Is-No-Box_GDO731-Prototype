using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;

    [SerializeField] int maxMoveDistance = 5;

    Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isActive) return;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float stopDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);

            ActionCompleted();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStarted(onActionComplete);
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitCurrentPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition availableGridPositon = new GridPosition(x, z);
                GridPosition validGridPosition = unitCurrentPosition + availableGridPositon;

                //Check for valid grid positions around unit, ignore out of bounds positions
                if (!LevelGrid.Instance.IsValidGridPosition(validGridPosition)) continue;

                //This is the same grid position as unit is already at, so should be ignored
                if (validGridPosition == unitCurrentPosition) continue;

                //Check if other Unit is already at position, if so cannot be occupied by current Unit
                if (LevelGrid.Instance.IsGridPositionOccupiedByUnit(validGridPosition)) continue;

                validGridPositionList.Add(validGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtPosition = unit.GetShootAction().GetTargetCountAtGridPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtPosition * 10
        };
    }
}
