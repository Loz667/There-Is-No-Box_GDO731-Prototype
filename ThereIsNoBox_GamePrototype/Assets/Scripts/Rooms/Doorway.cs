
using UnityEngine;

    public class Doorway : MonoBehaviour, IRaycastable
    {
        
        private enum CardinalPoint {None, North, East, South, West}

        [SerializeField] private CardinalPoint direction;
        
        
        public void HandleRaycast()
        {
            Debug.Log("Doorway HandleRaycast");
             
            RoomManager parentRoom = GetComponentInParent<RoomManager>();
            if (parentRoom != null)
            {
                //TODO Check that we are in the active room.    
                Debug.Log("Clicked on door in Room : " + parentRoom.roomPosition);
                Debug.Log("Current Room in Facility: " + Game.Director.ActiveRoom);
                if (parentRoom != Game.Director.ActiveRoom)
                {
                    Debug.LogWarning("Trying to move from a room that is not the currently active room");
                }
            }
            else
            {
                Debug.LogError("Cannot get handle on parent room");
            }
            Game.Facility.MoveToRoom(NextRoomVector());
        }

        private Vector2Int NextRoomVector()
        {
            return direction switch
            {
                CardinalPoint.North => Vector2Int.up,
                CardinalPoint.East => Vector2Int.right,
                CardinalPoint.South => Vector2Int.down,
                CardinalPoint.West => Vector2Int.left,
                _ => Vector2Int.zero
            };
        }
        
        


    }
