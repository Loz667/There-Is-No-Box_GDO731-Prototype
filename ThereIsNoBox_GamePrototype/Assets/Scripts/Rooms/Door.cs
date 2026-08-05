using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IRaycastable
{
    public RoomManager originRoom;
    public RoomManager targetRoom;    

    public async void HandleRaycast()
    {
        Debug.Log("Door HandleRaycast ASYNC");
        bool confirmMove = await PuzzleDemo.Instance.GoToNextRoomAsync();
        
        if (confirmMove)
        {
            await DoRoomTransition();
        }
    }

    private async Task DoRoomTransition()
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
