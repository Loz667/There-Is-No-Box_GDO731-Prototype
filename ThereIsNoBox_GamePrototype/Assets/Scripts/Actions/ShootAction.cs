using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler OnStartAim;
    public event EventHandler OnStopAim;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition unitCurrentPosition = unit.GetGridPosition();
        return GetValidGridPositionList(unitCurrentPosition);
    }

    public List<GridPosition> GetValidGridPositionList(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition availableGridPositon = new GridPosition(x, z);
                GridPosition validGridPosition = gridPosition + availableGridPositon;

                //Check for valid grid positions around unit, ignore out of bounds positions
                if (!LevelGrid.Instance.IsValidGridPosition(validGridPosition)) continue;

                int shootDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (shootDistance > maxShootDistance) continue;

                //Grid position not occuppied by Unit
                if (!LevelGrid.Instance.IsGridPositionOccupiedByUnit(validGridPosition)) continue;

                Unit target = LevelGrid.Instance.GetUnitAtOccupiedPosition(validGridPosition);

                //Both Units are enemy Units
                if (target.IsEnemy() == unit.IsEnemy()) continue;

                validGridPositionList.Add(validGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtOccupiedPosition(gridPosition);

        OnStartAim?.Invoke(this, EventArgs.Empty);

        currentState = State.Aiming;
        float aimingTime = 0.1f;
        stateTimer = aimingTime;

        canShoot = true;

        ActionStarted(onActionComplete);
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public int GetShootRange()
    {
        return maxShootDistance;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit targetUnit = LevelGrid.Instance.GetUnitAtOccupiedPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100 + Mathf.RoundToInt((1 - targetUnit.GetHealthNormalised()) * 100)
        };
    }

    public int GetTargetCountAtGridPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList(gridPosition).Count;
    }

    enum State { Aiming, Shooting, Cooldown }

    State currentState;

    int maxShootDistance = 7;
    float stateTimer;
    Unit targetUnit;
    bool canShoot;


    void Update()
    {
        if (!isActive) return;

        stateTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Aiming:
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * rotateSpeed);
                break;
            case State.Shooting:
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                }
                break;
            case State.Cooldown:
                OnStopAim?.Invoke(this, EventArgs.Empty);
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void Shoot()
    {
        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = targetUnit,
            shootingUnit = unit
        });

        targetUnit.DamageHealth(2);
    }

    private void NextState()
    {
        switch (currentState)
        {
            case State.Aiming:
                currentState = State.Shooting;
                float firingTime = 0.1f;
                stateTimer = firingTime;
                break;
            case State.Shooting:
                currentState = State.Cooldown;
                float cooldownTime = 0.5f;
                stateTimer = cooldownTime;
                break;
            case State.Cooldown:
                ActionCompleted();
                break;
        }
    }
}
