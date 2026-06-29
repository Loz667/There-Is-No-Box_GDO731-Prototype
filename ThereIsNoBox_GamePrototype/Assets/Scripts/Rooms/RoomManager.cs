using UnityEngine;
using Unity.Cinemachine;

public class RoomManager : MonoBehaviour
{
    
    [SerializeField] private CinemachineCamera roomCamera;
    [SerializeField] private Transform cameraTarget;
    
    
    void Start()
    {
        if(roomCamera != null && cameraTarget != null) roomCamera.Follow = cameraTarget;
    }


    public void SetActiveRoomCamera(bool isActive)
    {
        if (roomCamera != null)
        {
            roomCamera.Priority = isActive ? 20 : 0;
        }
    }
    
}
