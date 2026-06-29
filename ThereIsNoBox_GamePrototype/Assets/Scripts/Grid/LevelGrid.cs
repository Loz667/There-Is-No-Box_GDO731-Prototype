using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    public event EventHandler OnAnyUnitMovedGridPosition;

    [SerializeField] Transform debugGridObject;

    GridSystem gridSystem;

    void Awake()
    {
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(debugGridObject);
    }

    public void AddUnitAtGridPosition(GridPosition position, Unit unit)
    {
        GridObject grid = gridSystem.GetGridObject(position);
        grid.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition position)
    {
        GridObject grid = gridSystem.GetGridObject(position);
        return grid.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition position, Unit unit)
    {
        GridObject grid = gridSystem.GetGridObject(position);
        grid.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromPosition,  GridPosition toPosition)
    {
        RemoveUnitAtGridPosition(fromPosition, unit);

        AddUnitAtGridPosition(toPosition, unit);

        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool IsGridPositionOccupiedByUnit(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.OccupiedByUnit();
    }

    public Unit GetUnitAtOccupiedPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitAtPosition();
    }

    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();
}
