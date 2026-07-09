using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{    
    [SerializeField] private CinemachineCamera roomCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform characterSpawn;

    GridPosition roomPosition;
    List<Interactable> interactableObjects = new List<Interactable>();

    void Start()
    {
        roomPosition = FacilityGrid.Instance.GetGridPosition(transform.position);
        FacilityGrid.Instance.AddRoomAtGridPosition(roomPosition, this);

        if (roomCamera != null && cameraTarget != null) roomCamera.Follow = cameraTarget;

        CreateInteractableObjectsList(transform);
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
}
