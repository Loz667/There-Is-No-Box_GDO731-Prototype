using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { WaitingForTurn, TakingTurn, Busy }

    private State state;
    float timer;

    void Awake()
    {
        state = State.WaitingForTurn;
    }

    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
    }

    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()) return;

        switch (state)
        {
            case State.WaitingForTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    if (TryEnemyAITakeAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        //Enemies have taken all actions
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
    }

    void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
    }

    bool TryEnemyAITakeAction(Action onEnemyAIActionComplete)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitsList())
        {
            if (TryEnemyAITakeAction(enemyUnit, onEnemyAIActionComplete))
            {
                return true;
            }
        }
        return false;
    }

    bool TryEnemyAITakeAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;

        foreach (BaseAction baseAction in enemyUnit.GetBaseActions())
        {
            if (!enemyUnit.CanUsePointsToTakeAction(baseAction)) continue;

            if (bestEnemyAIAction == null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAIAction newEnemyAIAction = baseAction.GetBestEnemyAIAction();
                if (newEnemyAIAction != null && newEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = newEnemyAIAction;
                    bestBaseAction = baseAction;
                }
            }
        }

        if (bestEnemyAIAction != null && enemyUnit.TryUsePointsToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionComplete);
            return true;
        }
        else
        {
            return false;
        }
        //SpinAction spinAction = enemyUnit.GetSpinAction();

        //GridPosition actionGridPosition = enemyUnit.GetGridPosition();

        //if (!spinAction.IsValidGridPositionForAction(actionGridPosition)) return false;

        //if (!enemyUnit.TryUsePointsToTakeAction(spinAction)) return false;

        //Debug.Log("Spinning Around!");
        //spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        //return true;
    }
}
