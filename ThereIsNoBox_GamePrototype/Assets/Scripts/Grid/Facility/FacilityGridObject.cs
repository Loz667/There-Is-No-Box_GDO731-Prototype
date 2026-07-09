using System;
using System.Collections.Generic;
using UnityEngine;

public class FacilityGridObject
{
    FacilityGridSystem facilityGrid;
    GridPosition gridPosition;
    List<RoomManager> rooms;
    List<Unit> unitList;

    public FacilityGridObject(FacilityGridSystem facilityGrid, GridPosition gridPosition)
    {
        this.facilityGrid = facilityGrid;
        this.gridPosition = gridPosition;
        rooms = new List<RoomManager>();
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string roomName = "";
        foreach (RoomManager room in rooms)
        {
            roomName += room + "\n";
        }
        return gridPosition.ToString() + "\n" + roomName;
    }

    public void AddRoom(RoomManager room)
    {
        rooms.Add(room);
    }

    public void RemoveRoom(RoomManager room)
    {
        rooms.Remove(room);
    }

    public RoomManager GetRoomFromList(GridPosition position)
    {
        foreach (RoomManager room in rooms)
        {
            if (room.transform.position == facilityGrid.GetWorldPosition(position))
            {
                return room;
            }
        }
        return null;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
}
