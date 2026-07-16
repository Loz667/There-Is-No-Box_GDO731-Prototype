using System.Threading.Tasks;
using UnityEngine;

    public class Doorway : MonoBehaviour, IRaycastable
    {
   
        [SerializeField] private RoomManager.CardinalPoint direction;
        
        public void HandleRaycast()
        {
            Debug.Log("Doorway HandleRaycast");
             
            RoomManager parentRoom = GetComponentInParent<RoomManager>();
            if (parentRoom != null)
            {
                //TODO Check that we are in the active room.    
                Debug.Log("Clicked on door in Room : " + parentRoom.roomPosition);
                Debug.Log("Current Room in Facility: " + Game.Director.activeRoom);
                if (parentRoom != Game.Director.activeRoom)
                {
                    Debug.LogWarning("Trying to move from a room that is not the currently active room");
                }
            }
            else
            {
                Debug.LogError("Cannot get handle on parent room");
            }
           Game.Facility.MoveToRoom(direction);
        }
    }
