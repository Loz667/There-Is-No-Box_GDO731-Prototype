using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{    
    [SerializeField] private CinemachineCamera roomCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform characterSpawn;

    GridPosition roomPosition;
    List<Prop> props = new List<Prop>();

    void Start()
    {
        roomPosition = FacilityGrid.Instance.GetGridPosition(transform.position);
        FacilityGrid.Instance.AddRoomAtGridPosition(roomPosition, this);

        if (roomCamera != null && cameraTarget != null) roomCamera.Follow = cameraTarget;

        AddChildProps(transform);
        Debug.Log($"RoomManager: {gameObject.name} has {props.Count} props.");
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

    void AddChildProps(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Prop prop = child.GetComponent<Prop>();
            if (prop != null)
            {
                props.Add(prop);
            }
            AddChildProps(child);
        }
    }
}
