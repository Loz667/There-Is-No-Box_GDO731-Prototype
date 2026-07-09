using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;

    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(Action onActionComplete);

    //public virtual bool IsValidGridPositionForAction(GridPosition gridPosition)
    //{
    //    List<GridPosition> validGridPositionList = GetValidGridPositionList();
    //    return validGridPositionList.Contains(gridPosition);
    //}

    //public abstract List<GridPosition> GetValidGridPositionList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    //public EnemyAIAction GetBestEnemyAIAction()
    //{
    //    List<EnemyAIAction> enemyAIActions = new List<EnemyAIAction>();

    //    //List<GridPosition> validGridPositions = GetValidGridPositionList();

    //    foreach (GridPosition gridPosition in validGridPositions)
    //    {
    //        EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
    //    }

    //    enemyAIActions.Add(enemyAIAction);

    //    if (enemyAIActions.Count > 0)
    //    {
    //        enemyAIActions.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);

    //        return enemyAIActions[0];
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);

    protected void ActionStarted(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionCompleted()
    {
        isActive = false;
        onActionComplete();

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }
}
