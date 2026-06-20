using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] int actionPoints = 2;

    GridPosition currentPosition;

    MoveAction moveAction;
    SpinAction spinAction;
    BaseAction[] baseActions;

    int maxActionPoints;

    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActions = GetComponents<BaseAction>();
    }

    public bool TryUsePointsToTakeAction(BaseAction baseAction)
    {
        if (CanUsePointsToTakeAction(baseAction))
        {
            UseActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanUsePointsToTakeAction(BaseAction baseAction)
    {
        return actionPoints >= baseAction.GetActionPointsCost();
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public BaseAction[] GetBaseActions()
    {
        return baseActions;
    }

    public GridPosition GetGridPosition()
    {
        return currentPosition;
    }

    void Start()
    {
        currentPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentPosition, this);
        
        maxActionPoints = actionPoints;

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
    }

    void Update()
    {
        GridPosition newPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newPosition != currentPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, currentPosition, newPosition);
            currentPosition = newPosition;
        }
    }

    void UseActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        actionPoints = maxActionPoints;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}
