using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

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
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
}
