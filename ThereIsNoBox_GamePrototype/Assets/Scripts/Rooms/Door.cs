using System.Threading.Tasks;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IRaycastable
{
    public RoomManager originRoom;
    public RoomManager targetRoom;    

    public void HandleRaycast()
    {
        DoRoomTransition();
    }

    private async void DoRoomTransition()
    {
        await ScreenFader.Instance.FadeOut();
        originRoom.SetActiveRoomCamera(false);
        SpawnCharacterInRoom();
        targetRoom.SetActiveRoomCamera(true);
        await Task.Delay(150);
        await ScreenFader.Instance.FadeIn();
    }

    void SpawnCharacterInRoom()
    {
        NavMeshAgent player = UnitActionSystem.Instance.GetSelectedUnit().GetComponent<NavMeshAgent>();

        player.enabled = false;
        player.transform.position = targetRoom.GetSpawnLocation();
        player.enabled = true;
    }
}
