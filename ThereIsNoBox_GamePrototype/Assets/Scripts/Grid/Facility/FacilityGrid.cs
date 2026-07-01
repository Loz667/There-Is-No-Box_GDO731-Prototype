using System;
using System.Collections.Generic;
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

    public GridPosition GetGridPosition(Vector3 worldPosition) => facilityGrid.GetGridPosition(worldPosition);
}
