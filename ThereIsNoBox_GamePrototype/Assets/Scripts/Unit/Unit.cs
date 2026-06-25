using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyHealthPointsChanged;
    public static event EventHandler OnAnyMoralePointsChanged;

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

    public int GetHealthPoints()
    {
        return healthSystem.GetHealth();
    }

    public int GetMoralePoints()
    {
        return moraleSystem.GetMorale();
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public ShootAction GetShootAction()
    {
        return shootAction;
    }

    public BaseAction[] GetBaseActions()
    {
        return baseActions;
    }

    public GridPosition GetGridPosition()
    {
        return currentPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void DamageHealth(int damageAmount)
    {
        healthSystem.DamageHealth(damageAmount);

        OnAnyHealthPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealth()
    {
        return healthSystem.GetHealth();
    }

    public float GetHealthNormalised()
    {
        return healthSystem.GetHealthNormalised();
    }

    public void DamageMorale(int moraleAmount)
    {
        moraleSystem.DamageMorale(moraleAmount);

        OnAnyMoralePointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetMorale()
    {
        return moraleSystem.GetMorale();
    }

    public float GetMoraleNormalised()
    {
        return moraleSystem.GetMoraleNormalised();
    }


    [SerializeField] bool isEnemy;
    [SerializeField] int actionPoints = 2;

    HealthSystem healthSystem;
    MoraleSystem moraleSystem;

    GridPosition currentPosition;

    MoveAction moveAction;
    SpinAction spinAction;
    ShootAction shootAction;
    BaseAction[] baseActions;

    int maxActionPoints;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        moraleSystem = GetComponent<MoraleSystem>();

        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        shootAction = GetComponent<ShootAction>();
        baseActions = GetComponents<BaseAction>();
    }

    void Start()
    {
        currentPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentPosition, this);

        maxActionPoints = actionPoints;

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;

        healthSystem.OnHealthDepleted += OnHealthDepleted;
        moraleSystem.OnMoraleDepleted += OnMoraleDepleted;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        GridPosition newPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newPosition != currentPosition)
        {
            GridPosition oldPosition = currentPosition;
            currentPosition = newPosition;

            LevelGrid.Instance.UnitMovedGridPosition(this, oldPosition, newPosition);
        }
    }

    void UseActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = maxActionPoints;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    void OnHealthDepleted(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(currentPosition, this);

        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    void OnMoraleDepleted(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(currentPosition, this);

        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }
}
