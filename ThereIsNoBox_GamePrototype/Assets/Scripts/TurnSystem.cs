using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    int turnNumber = 1;
    bool isPlayerTurn = true;

    void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit().HasTakenAction)
        {
            UnitActionSystem.Instance.GetNextActiveUnit();

            if (CheckIfAllPlayerUnitsHaveTakenAction())
            {
                NextTurn();
            }
        }
    }

    public void NextTurn()
    {
        turnNumber++;

        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    bool CheckIfAllPlayerUnitsHaveTakenAction()
    {
        foreach (Unit unit in UnitManager.Instance.GetPlayerUnitsList())
        {
            if (!unit.HasTakenAction)
            {
                return false;
            }
        }
        return true;
    }
}
