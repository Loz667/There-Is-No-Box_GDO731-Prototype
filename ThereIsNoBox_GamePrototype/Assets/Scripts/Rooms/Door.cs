using System.Threading.Tasks;
using UnityEngine;
using Unity.Cinemachine;

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
        targetRoom.SetActiveRoomCamera(true);
        await Task.Delay(150);
        await ScreenFader.Instance.FadeIn();
    }
    
    
}
