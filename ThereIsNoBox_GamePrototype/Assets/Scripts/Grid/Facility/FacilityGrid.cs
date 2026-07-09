using UnityEngine;

public class FacilityGrid : MonoBehaviour
{
    public static FacilityGrid Instance { get; private set; }

    [SerializeField] Transform debugGridObject;

    FacilityGridSystem facilityGrid;

    void Awake()
    {
        Instance = this;

        facilityGrid = new FacilityGridSystem(10, 10, 50f);
        facilityGrid.CreateDebugObjects(debugGridObject);
    }

    public void AddRoomAtGridPosition(GridPosition position, RoomManager room)
    {
        FacilityGridObject grid = facilityGrid.GetFacilityGridObject(position);
        grid.AddRoom(room);
    }

    public RoomManager GetRoomAtGridPosition(GridPosition position)
    {
        FacilityGridObject grid = facilityGrid.GetFacilityGridObject(position);
        return grid.GetRoomFromList(position);
    }

    public void AddUnitAtGridPosition(GridPosition position, Unit unit)
    {
        FacilityGridObject currentRoom = facilityGrid.GetFacilityGridObject(position);
        currentRoom.AddUnit(unit);
    }

    public void RemoveUnitAtGridPosition(GridPosition position, Unit unit)
    {
        FacilityGridObject currentRoom = facilityGrid.GetFacilityGridObject(position);
        currentRoom.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromPosition, GridPosition toPosition)
    {
        RemoveUnitAtGridPosition(fromPosition, unit);

        AddUnitAtGridPosition(toPosition, unit);

        //OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => facilityGrid.GetGridPosition(worldPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => facilityGrid.IsValidGridPosition(gridPosition);
}
