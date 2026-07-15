using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public enum CardinalPoint {None, North, East, South, West}
    
    [Header("Room Definition")] 
    [SerializeField] private RoomDef roomData;
    
    [System.Serializable]
    public struct DoorWay
    {
        public GameObject wallObject; 
        public GameObject doorwayObject;
        public GameObject doorObject;
        public Transform spawnPoint;  
    }
    
    [SerializeField] private CinemachineCamera roomCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform characterSpawn;
    
    [Header("Exits (North = +Z, South = -Z, East = +X, West = -X)")]
    [SerializeField] private DoorWay northExit;
    [SerializeField] private DoorWay southExit;
    [SerializeField] private DoorWay eastExit;
    [SerializeField] private DoorWay westExit;
    
    [Header("Where characters should stand")]
    [SerializeField] public List<Transform> PlayerPoint;
    
    public GridPosition roomPosition { get; private set; }
    List<Interactable> interactableObjects = new List<Interactable>();
    
    List<Character> characterList = new List<Character>();

    void Start()
    {
        roomPosition = FacilityGrid.Instance.GetGridPosition(transform.position);
        FacilityGrid.Instance.AddRoomAtGridPosition(roomPosition, this);

        if (roomCamera != null && cameraTarget != null) roomCamera.Follow = cameraTarget;

        CreateInteractableObjectsList(transform);
    }

    public void Initialize(Vector2Int roomPos, bool hasNorth, bool hasSouth, bool hasEast, bool hasWest)
    {
        roomPosition = new GridPosition(roomPos.x, roomPos.y);
        if (roomData == null)
        {
            roomData = ScriptableObject.CreateInstance<RoomDef>();
            roomData.RoomName = "TempRoom_{roomPos.x}_{roomPos.y}";
            roomData.RoomDescription = "Placeholder room";
        }
        
        gameObject.name = $"Room_{roomPos.x}_{roomPos.y} ({roomData.RoomName})";

        InitializeDoorway(northExit, hasNorth);
        InitializeDoorway(southExit, hasSouth);
        InitializeDoorway(eastExit, hasEast);
        InitializeDoorway(westExit, hasWest);
        
    }
    
    private void InitializeDoorway(DoorWay exit, bool neighborExists)
    {
        if (exit.doorwayObject != null)
        {
            exit.doorwayObject.SetActive(neighborExists);
            exit.doorObject.SetActive(neighborExists);
        }
        if (exit.wallObject != null) exit.wallObject.SetActive(!neighborExists);
        else if (!neighborExists && exit.doorObject != null) exit.doorObject.SetActive(false); // Turn off doors on South/West walls
    }
    
    public void SetActiveRoomCamera(bool isActive)
    {
        if (roomCamera != null)
        {
            roomCamera.Priority = isActive ? 20 : 0;
        }
    }
    
    public Vector3 GetSpawnLocation()
    {
        return characterSpawn.position;
    }

    public Vector3 GetCharacterStand(int idx)
    {
        if (idx < 0 && idx > PlayerPoint.Count)
        {
            Debug.LogError("Character stand index out of range");
            return Vector3.zero;
        }

        return PlayerPoint[idx].position;
    }
    public Transform GetRoomEntryPoint(Vector2Int incomingDirection)
    {
        if (incomingDirection == Vector2Int.up) return southExit.spawnPoint;
        if (incomingDirection == Vector2Int.down) return northExit.spawnPoint;
        if (incomingDirection == Vector2Int.right) return westExit.spawnPoint;
        if (incomingDirection == Vector2Int.left) return eastExit.spawnPoint;
        
        return transform;
    }

    public void AddCharacter(Character character) => characterList.Add(character);
    public void RemoveCharacter(Character character) => characterList.Remove(character);

    void CreateInteractableObjectsList(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Interactable obj = child.GetComponent<Interactable>();
            if (obj != null)
            {
                interactableObjects.Add(obj);
            }
            CreateInteractableObjectsList(child);
        }
    }
    
    public static Vector2Int NextRoomVector(CardinalPoint direction)
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
