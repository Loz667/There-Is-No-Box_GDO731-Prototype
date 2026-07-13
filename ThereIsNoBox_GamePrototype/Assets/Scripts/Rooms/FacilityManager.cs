using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class FacilityManager : MonoBehaviour
{
    
    [SerializeField] private int maxGridWidth = 3;
    //[SerializeField] private int maxGridHeight = 3;

    [System.Serializable]
    public struct RoomMapping
    {
        public Vector2Int gridPosition;
        public RoomManager roomInstance;
    } 
    
    [SerializeField] private List<RoomMapping> roomMappings = new  List<RoomMapping>();

    private RoomManager[,] roomGrid;
    //public RoomManager ActiveRoom {get; private set;}
    private RoomManager focusedRoom;
    
    private void Start()
    {
        
    }

    public RoomManager Initialize()
    {
        BuildFacilityGrid();
        RoomManager startRoom =  roomGrid[0, 0];
        //Game.Director.ActiveRoom  = startRoom;
        //startRoom.SetActiveRoomCamera(true);
        return startRoom;
    }

    private void BuildFacilityGrid()
    {
        roomGrid = new RoomManager[maxGridWidth, maxGridWidth];
        foreach (RoomMapping roomMapping in roomMappings)
        {
            Vector2Int gridPosition = roomMapping.gridPosition;
            if (gridPosition.x >= 0 && 
                gridPosition.x < maxGridWidth && 
                gridPosition.y >= 0 && 
                gridPosition.y < maxGridWidth)
            {
                roomGrid[gridPosition.x, gridPosition.y] = roomMapping.roomInstance;
                bool hasNorth = gridPosition.y + 1 < maxGridWidth;
                bool hasEast = gridPosition.x + 1 < maxGridWidth;
                bool hasSouth = gridPosition.y - 1 >= 0;
                bool hasWest = gridPosition.x - 1 >= 0;
                
                roomMapping.roomInstance.Initialize(gridPosition, hasNorth, hasSouth, hasEast, hasWest);
                roomMapping.roomInstance.SetActiveRoomCamera(false);
            }
        }
    }

    public RoomManager GetRoomAtPosition(int x, int y)
    {
        return roomGrid[x, y];
    }

    //This will allow camera movement between rooms without moving a character
    public async void FocusOnRoom(Vector2Int focusRoomPosition)
    {
        
        RoomManager newRoom = roomGrid[focusRoomPosition.x, focusRoomPosition.y];
    }


    public async void MoveToRoom(Vector2Int nextRoomDirection)
    {
        //TODO Manage active room - needs to be which ever room the active character is in
        
        
        GridPosition nextRoomPosition = Game.Director.ActiveRoom.roomPosition + new GridPosition(nextRoomDirection.x, nextRoomDirection.y);
        Debug.Log("DoRoomTransition " + nextRoomDirection);
        //TODO Check that requested direction leads to a valid destination
        
        RoomManager nextRoom = roomGrid[nextRoomPosition.x, nextRoomPosition.z];

        if (nextRoom == null)
        {
            Debug.LogError("DoRoomTransition nextRoom is null: " + nextRoomPosition);
            return;
        }
        
        //TODO This whole thing needs to be a co-routine to handle unit movement, room transition etc. 
        await ScreenFader.Instance.FadeOut();
        Game.Director.ActiveRoom.SetActiveRoomCamera(false);
        //SpawnCharacterInRoom();
        nextRoom.SetActiveRoomCamera(true);
        await Task.Delay(150);
        await ScreenFader.Instance.FadeIn();
        
        Game.Director.ActiveRoom = nextRoom;
        
    }
    
    
}
