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


    //public async void MoveToRoom(Vector2Int nextRoomDirection)
    public async void MoveToRoom(RoomManager.CardinalPoint direction)
    {
        
        bool confirmMove = await PuzzleDemo.Instance.GoToNextRoomAsync();

        if (!confirmMove)
        {
            Debug.Log("Room transition canceled by player.");
            return;
        }
        
        //TODO Manage active room - needs to be which ever room the active character is in
        Vector2Int nextRoomDirection = RoomManager.NextRoomVector(direction); 
        
        GridPosition nextRoomPosition = Game.Director.activeRoom.roomPosition + new GridPosition(nextRoomDirection.x, nextRoomDirection.y);
        Debug.Log("DoRoomTransition " + nextRoomDirection);
        //TODO Check that requested direction leads to a valid destination
        
        RoomManager nextRoom = roomGrid[nextRoomPosition.x, nextRoomPosition.z];

        if (nextRoom == null)
        {
            Debug.LogError("DoRoomTransition nextRoom is null: " + nextRoomPosition);
            return;
        }
        
        Transform exitPoint = Game.Director.activeRoom.GetRoomEntryPoint(-nextRoomDirection);
        Transform entryPoint = nextRoom.GetRoomEntryPoint(nextRoomDirection);
        
        Debug.Log("Exit from : " + exitPoint.name + " " + exitPoint.position);
        Debug.Log("Enter into: " + entryPoint.name);
        
        Mover movable = Game.Director.ActiveCharacter.GetComponent<Mover>();
        if (movable == null) return;
        
        bool atExit = await movable.MoveToAsync(exitPoint.position);
        Debug.Log("Finished moving");
        if (atExit)
        {
            //TODO This whole thing needs to be a co-routine to handle unit movement, room transition etc. 
            await DoRoomTransition(nextRoom, movable, entryPoint.position);
            movable.MoveTo(nextRoom.GetCharacterStand(Game.Director.CharacterIndex));
            Game.Director.activeRoom = nextRoom;
        }
        else
        {
            Debug.Log("Character not at exit?");
        }
        
    }

    private async Task DoRoomTransition(RoomManager nextRoom, Mover mover, Vector3 target)
    {
        Debug.Log("DoRoomTransition");
        if (nextRoom == null)
        {
            Debug.Log("nextRoom is null");
            return;
        }
        RoomManager oldRoom = Game.Director.activeRoom;
        await ScreenFader.Instance.FadeOut();
        if(oldRoom != null) oldRoom.SetActiveRoomCamera(false);
        
        mover.WarpToPoint(target);
        Game.Director.ActiveCharacter.EnterRoom(nextRoom);
        
        nextRoom.SetActiveRoomCamera(true);
        
        await Task.Delay(150);
        await ScreenFader.Instance.FadeIn();
    }
}
