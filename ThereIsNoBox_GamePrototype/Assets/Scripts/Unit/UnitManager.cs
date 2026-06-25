using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public List<Unit> GetAllUnitsList()
    {
        return allUnitsList;
    }

    public List<Unit> GetPlayerUnitsList()
    {
        return playerUnitsList;
    }

    public List<Unit> GetEnemyUnitsList()
    {
        return enemyUnitsList;
    }

    List<Unit> allUnitsList;
    List<Unit> playerUnitsList;
    List<Unit> enemyUnitsList;

    void Awake()
    {
        Instance = this;

        allUnitsList = new List<Unit>();
        playerUnitsList = new List<Unit>();
        enemyUnitsList = new List<Unit>();
    }

    void Start()
    {
        Unit.OnAnyUnitSpawned += OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += OnAnyUnitDead;
    }

    private void OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        allUnitsList.Add(unit);

        if (unit.IsEnemy())
        {
            enemyUnitsList.Add(unit);
        }
        else
        {
            playerUnitsList.Add(unit);
        }
    }

    private void OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        allUnitsList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyUnitsList.Remove(unit);
        }
        else
        {
            playerUnitsList.Remove(unit);
        }
    }
}
