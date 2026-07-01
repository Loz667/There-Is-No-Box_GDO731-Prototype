using System.Collections.Generic;
using UnityEngine;

public class FacilityGridObject
{
    FacilityGridSystem facilityGrid;
    GridPosition gridPosition;
    List<RoomManager> rooms;

    public FacilityGridObject(FacilityGridSystem facilityGrid, GridPosition gridPosition)
    {
        this.facilityGrid = facilityGrid;
        this.gridPosition = gridPosition;
        rooms = new List<RoomManager>();
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
}
