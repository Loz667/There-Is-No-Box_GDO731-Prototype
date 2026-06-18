using System.Collections.Generic;

public class GridObject
{
    GridSystem gridSystem;
    GridPosition gridPosition;
    List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitName = "";
        foreach (Unit unit in unitList)
        {
            unitName += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitName;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }
}
